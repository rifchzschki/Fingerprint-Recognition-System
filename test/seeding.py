import mysql.connector
from faker import Faker
import datetime
import random
import csv
import re  # To define regex patterns for filtering unwanted characters

# Initialize Faker
faker = Faker()

# # Read the CSV into a list
# with open('steam2.csv', 'r', newline='', encoding='utf-8') as file:
#     csv_reader = csv.reader(file)
#     words = [row[0] for row in csv_reader]  # Extract the first column

# # Filter words that don't contain commas or other unwanted characters
# pattern = re.compile(r'^[^\W,]+$', re.ASCII)  # Regex to only allow alphanumeric and no commas
# filtered_words = [word for word in words if pattern.match(word)]

# # Get unique words from the filtered list
# word_list = list(set(filtered_words))

# # Set to ensure unique names
# used_names = set()

# # Function to generate a unique name from the word list
# def generate_unique_name():
#     while True:
#         word = random.choice(word_list)  # Choose a random word from the list
#         if word not in used_names:
#             used_names.add(word)  # Add to the set of used names
#             return word

# Function to generate a unique NIK
unique_nik = set()
def generate_unique_nik():
    while True:
        nik = faker.numerify(text='##############')  # Generate a unique 13-digit number
        # nik = faker.unique.numerify(text='###############')  # Generate a unique 16-digit number
        if nik not in unique_nik:
            unique_nik.add(nik)  # Add to the set of unique NIKs
            return nik

# Function to generate a random date between two dates
def random_date(start, end):
    delta = end - start
    random_days = random.randint(0, delta.days)
    return start + datetime.timedelta(days=random_days)

# Connect to the MariaDB database
conn = mysql.connector.connect(
    host='localhost',  # Change if needed
    user='root',  # Replace with your username
    # password='Ma17urungh3bat',  # Replace with your password
    database='stima3',  # Your database name
    port=3306  # Change if needed
)

cursor = conn.cursor()

# Number of dummy records to generate
num_records = 10  # Number of records to generate

# name array
names = ['John Doe', 'Jane Smith', 'Alice Johnson', 'Robert Brown', 'Michael Williams', 'David Jones', 'Emily Garcia', 'Sophia Martinez', 'Daniel Wilson', 'Mia Anderson']


# alay name array
alay_names = ['jHn d', 'j4n smith', '4lC jhn50n', 'Rbrt BRwN', 'm1chl WLl1m5', 'Davd jn35', 'M1LY 64rc', '5PH mrtN2', 'DNl Wl5N', 'm 4Ndr5n']

# Insert generated data into the 'biodata' table
start_date = datetime.date(1950, 1, 1)  # Start date for 'tanggal_lahir'
end_date = datetime.date(2010, 1, 1)  # End date for 'tanggal_lahir'

# Generate unique records for 'biodata' table
for i in range(num_records):
    # Generate a unique NIK
    nik = generate_unique_nik()

    # Generate random name from the predefined word list
    nama = random.choice(alay_names)

    # Reduce alay name
    alay_names.remove(nama)
    
    # Generate other attributes using Faker
    tempat_lahir = faker.city()
    tanggal_lahir = random_date(start_date, end_date)
    jenis_kelamin = random.choice(['Laki-Laki', 'Perempuan'])
    golongan_darah = random.choice(['A', 'B', 'AB', 'O'])
    alamat = faker.address().replace('\n', ', ')
    agama = random.choice(['Islam', 'Kristen', 'Katolik', 'Hindu', 'Budha', 'Konghucu'])
    status_perkawinan = random.choice(['Belum Menikah', 'Menikah', 'Cerai'])
    pekerjaan = faker.job()
    kewarganegaraan = random.choice(['Indonesia', 'Jepang', 'Amerika', 'Inggris', 'Australia', 'Korea', 'China', 'India', 'Rusia', 'Arab'])

    # Insert into the 'biodata' table
    cursor.execute(
        "INSERT INTO biodata (NIK, nama, tempat_lahir, tanggal_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan) VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)",
        (nik, nama, tempat_lahir, tanggal_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan)
    )

# Generate unique records for 'sidik_jari' table
for i in range(num_records):
    # Generate random name from the predefined word list
    nama = names[i]

    # Generate random image file path
    berkas_citra = 'test/sidik_jari' + str(i) + '.jpg'

    # Insert into the 'sidik_jari' table
    cursor.execute(
        "INSERT INTO sidik_jari (berkas_citra, nama) VALUES (%s, %s)",
        (berkas_citra, nama)
    )

# Commit the changes
conn.commit()

# Close the connection
cursor.close()
conn.close()
