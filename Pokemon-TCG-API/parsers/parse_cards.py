import json
import requests

def download_images(cards: list[dict], filepath: str):
  print(f"Downloading images for {len(cards)} cards")
  for card in cards:
    card_name = card['name']
    card_id = card['id']
    card_image_url = card['images']['large']
    card_set_id = card['set']['id']
    card_set_number = card['number']
    print(f"Saving image of card '{card_name}' with id '{card_id}' to '{filepath}'")
    try:
      image_data = requests.get(card_image_url).content
      with open(f"{filepath}/{card_set_id}.{card_set_number}.png", 'wb') as handler:
        handler.write(image_data)
    except Exception:
      print(f"Failed to fetch image for card '{card_name}' with id '{card_id}'")
    print(card_image_url)

def parse_cards_json(filepath: str, fetch_images: bool = False):
  with open(filepath) as file:
    data = json.load(file)
    cards = data['data']

    if fetch_images:
      download_images(cards, '../../assets/cards/base1')

def main():
  parse_cards_json(filepath='../BaseSetAPIResponse.json')

if __name__ == "__main__":
  main()