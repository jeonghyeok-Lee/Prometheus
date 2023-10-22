import random
import json

# 원하는 데이터의 개수 설정
num_points = 10000

# 랜덤 값을 생성하여 데이터 구성
data = {"Points": [{"X": round(random.uniform(-300, 300), 2),
                    "Y": round(random.uniform(-300, 300), 2),
                    "Z": round(random.uniform(-300, 300), 2)}
                   for _ in range(num_points)]}

# JSON 파일로 저장
output_filename = 'output1.json'
with open(output_filename, 'w') as json_file:
    json.dump(data, json_file, indent=2)

print(f"데이터가 {output_filename} 파일로 저장되었습니다.")
