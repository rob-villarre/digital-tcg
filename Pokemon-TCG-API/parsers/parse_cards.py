import json
import requests
import unicodedata

def download_images(cards: list[dict], filepath: str):
    """
    Downloads card images from provided URLs and saves them to specified filepath.
    
    Args:
        cards (list[dict]): List of card dictionaries containing image URLs and metadata
        filepath (str): Directory path where images should be saved
        
    Returns:
        None
    """
    print(f"Downloading images for {len(cards)} cards")
    for card in cards:
        card_name = card['name']
        card_id = card['id']
        card_image_url = card['images']['large']
        card_set_id = card['set']['id']
        card_set_number = card['number']
        print(f"Saving image of card '{card_name}' with id '{card_id}' to '{filepath}'")
        try:
            image_data = requests.get(card_image_url, timeout=10).content
            with open(f"{filepath}/{card_set_id}.{card_set_number}.png", 'wb') as handler:
                handler.write(image_data)
        except requests.RequestException:
            print(f"Failed to fetch image for card '{card_name}' with id '{card_id}'")

def normalize(text):
    """
    Normalizes text by removing diacritics and converting to lowercase.
    
    Args:
        text (str): The text string to normalize
        
    Returns:
        str: The normalized text with diacritics removed and converted to lowercase
    """
    return ''.join(
        c for c in unicodedata.normalize('NFKD', text)
        if not unicodedata.combining(c)
    ).lower()

def parse_cards_json(input_filepath: str, output_filepath: str, fetch_images: bool = False):
    """
    Parses a JSON file containing Pokemon card data.

    Args:
        filepath (str): Path to the JSON file to parse
        fetch_images (bool, optional): Whether to download card images. Defaults to False.

    Returns:
        None
    """
    with open(input_filepath, encoding='utf-8') as file:
        data = json.load(file)
        cards = data['data']

        if fetch_images:
            download_images(cards, '../../assets/cards/base1')
        
        print("Parsing all cards...")
        output = []
        for card in cards:
            print(f"Parsing card {card['id']}")
            parsed_card = {
                'id': card['id'],
                'name': card['name'],
                'number': card['number'],
                'supertype': normalize(card['supertype']),
                'set': card['set'],
            }

            # remove some fields from set
            del parsed_card['set']['images']
            

            optional_fields = ['subtypes', 'hp', 'level', 'types', 'evolvesFrom']
            for field in optional_fields:
                if field in card:
                    parsed_card[field] = card[field]

            if 'abilities' in card:
                abilities = card['abilities']
                for ability in abilities:
                    ability['type'] = normalize(ability['type'])
                parsed_card['abilities'] = abilities

            if 'attacks' in card:
                parsed_card['attacks'] = []
                attacks = card['attacks']
                for attack in attacks:
                    attack['baseDamage'], attack['damageOperation'] = parse_damage(attack['damage'])
                    parsed_card['attacks'].append(attack)

            if 'weaknesses' in card:
                parsed_card['weaknesses'] = []
                weaknesses = card['weaknesses']
                for weakness in weaknesses:
                    weakness['weaknessValue'], weakness['weaknessOperation'] = parse_weakness(weakness['value'])
                    parsed_card['weaknesses'].append(weakness)

            if 'resistances' in card:
                parsed_card['resistances'] = []
                resistances = card['resistances']
                for resistance in resistances:
                    resistance['resistanceValue'], resistance['resistanceOperation'] = parse_resistance(resistance['value'])
                    parsed_card['resistances'].append(resistance)


            output.append(parsed_card)
        
        print(f"Writting parsed cards json to {output_filepath}")
        json_string = json.dumps(output, indent=4, ensure_ascii=False)
        with open(output_filepath, 'w', encoding='utf-8') as output_file:
            output_file.write(json_string)

def parse_damage(damage: str) -> tuple[int, str]:
    """
    Parses damage string into base damage value and operation.
    
    Args:
        damage (str): Damage string to parse
        
    Returns:
        tuple[int, str]: Base damage value and damage operation
    """
    if not damage:
        return 0, 'none'

    if damage.endswith('+'):
        return int(damage.rstrip('+')), 'plus'

    if damage.endswith('×'):
        return int(damage.rstrip('×')), 'multiply'
    if damage.endswith('-'):
        return int(damage.rstrip('-')), 'minus'

    return int(damage), 'none'

def parse_weakness(value: str) -> tuple[int, str]:
    """
    Parses weakness value string into base weakness value and operation.
    
    Args:
        value (str): Weakness value string to parse
        
    Returns:
        tuple[int, str]: Base weakness value and weakness operation
    """

    if not value:
        return 0, 'none'

    if value.startswith("×"):
        return int(value.lstrip('×')), 'multiply'

    if value.startswith("+"):
        return int(value.lstrip('+')), 'plus'

    return int(value), 'none'

def parse_resistance(value: str) -> tuple[int, str]:
    """
    Parses resistance value string into base resistance value and operation.
    
    Args:
        value (str): Resistance value string to parse
        
    Returns:
        tuple[int, str]: Base resistance value and resistance operation
    """

    if not value:
        return 0, 'none'

    if value.startswith("-"):
        return int(value.lstrip('-')), 'minus'

    return int(value), 'none'

if __name__ == "__main__":
    parse_cards_json(
        input_filepath='../BaseSetAPIResponse.json',
        output_filepath='../../assets/cards/base1.json',
        fetch_images=False
    )
