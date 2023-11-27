import os
import json
import re

directory_path = r'C:\GitWorkSpace\Prometheus\Assets\Resources\json\data1'

def extract_number_from_filename(filename):
    match = re.search(r'depth_data_(\d+)', filename)
    if match:
        return int(match.group(1))
    return None

def update_location_x(file_path, increment, count):
    with open(file_path, 'r') as file:
        data = json.load(file)

    print(count * increment)
    # Increment the 'x' value in the 'location' dictionary
    data['location']['x'] = 0
    data['location']['r'] = count * increment

    with open(file_path, 'w') as file:
        json.dump(data, file, indent=2)

def update_location_x_in_directory(directory, increment):
    files = [f for f in os.listdir(directory) if os.path.isfile(os.path.join(directory, f)) and f.lower().endswith('.json')]
    files.sort(key=lambda x: extract_number_from_filename(x))

    count = 0
    for filename in files:
        file_path = os.path.join(directory, filename)

        # Check if the file is a JSON file
        if os.path.isfile(file_path) and file_path.lower().endswith('.json'):
            update_location_x(file_path, increment, count)
            count += 1

update_location_x_in_directory(directory_path, increment=20)
