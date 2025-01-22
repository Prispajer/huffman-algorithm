<div align="center">
  <h1 style="display: inline; vertical-align: middle;">Huffman Algorithm - Data Coding & Decoding</h1>
</div>

# Introduction
**Huffman Algorithm** is a data compression tool that implements the Huffman coding technique to reduce the size of input data (such as text files, binary files, or images). The project includes both encoding and decoding capabilities, allowing users to compress data efficiently while maintaining its integrity. 

# Key Features
The application includes the following features:

## Compression Features
- **Text Compression**: Compress text data using Huffman coding.
- **Binary and Image File Support**: Compress binary and image files using the same Huffman algorithm.
- **File Upload**: Upload files (text, binary, images) for compression.
- **Text Area**: Input text data and view encoded/decoded results.
- **Tree Visualization**: View a visual representation of the Huffman tree during the compression process.
- **Decompression**: Decompress compressed text, binary, or image files back to their original form.

## Algorithm Features
- **Frequency Calculation**: The algorithm counts the frequency of characters or symbols in the input data.
- **Binary Code Assignment**: More frequent characters are assigned shorter binary codes, reducing the overall size.
- **Lossless Compression**: Ensure data integrity during the compression and decompression process.

## User Interface
- **Input Fields**: Text input and file upload for compression and decompression tasks.
- **Encoded/Decoded Display**: View encoded or decoded output.
- **Huffman Tree Visualization**: Interactive visual representation of the Huffman tree.
- **Progress Indicator**: See real-time status while encoding or decoding data.
  
# Application Screenshots
- **Home Page**  
  ![Home Page](https://drive.usercontent.google.com/download?id=11TMJaq8iJ68UsQ_bWTpoRA5EQY1Toj7-&export=view&authuser=0)

- **Encoded Data**  
  ![Encoded Data](https://drive.usercontent.google.com/download?id=1jRmZBKmuqlITgBkAwErLfrZGNY3E5cME&export=view&authuser=0)

- **Decoded Data**  
  ![Decoded Data](https://drive.usercontent.google.com/download?id=11OtEudVZRsY8Gd4rJ1TXGJ1qIaj59Ku6&export=view&authuser=0)

- **Huffman Tree Visualization**  
  ![Huffman Tree Visualization](https://drive.usercontent.google.com/download?id=1MGoHG3fUuNN_govIkIXCfAQ6LNCHo8D7&export=view&authuser=0)

- **File Management**  
  ![File Management](https://drive.usercontent.google.com/download?id=1gl42-7wwWCMLYMkqW10FP8DU0HjbzV5W&export=view&authuser=0)

# Application Tech Stack
  - C#
  - ASP.NET 
  - NPM
  - DotNetGraph 
  - Viz.js 
  - Blazor
    
# Algorithm logic:
1. **Counting symbol frequencies**: The first step is to count how often a given character or byte appears in the data.
2. **Building the Huffman tree**: Based on symbol frequencies, a binary tree is built, where lower-frequency symbols are placed closer to the leaves.
3. **Assigning binary codes**: After constructing the tree, symbols are assigned binary codes based on their positions in the tree (e.g., "0" for left branches and "1" for right branches).
4. **Data compression**: Input data is compressed by replacing symbols with their respective binary codes.
5. **Data decompression**: To restore the original data, simply follow the tree path using the encoded bits.

# System Architecture
The application is designed in a modular way, with clear separation of responsibilities among classes and interfaces. The key components of the system include:

- **HuffmanDecoderService** – for trigger decoding data.
- **HuffmanDecodingService** - for decoding data.
- **HuffmanEncoderService** – for trigger encoding data.
- **HuffmanEncodingService** - for encoding data.
- **HuffmanGraphService** – for generating and visualizing the Huffman tree.
- **HuffmanFileService** – for handling input and output files.

# Project Structure:
- **wwwroot**: Static resources (images, CSS, JS files).
- **Components**: User interface components.
- **Interfaces**: Service interfaces defining contracts that must be implemented.
- **Layout**: Layout files, representing the common structure of the user interface.
- **Models**: Data structures for the application.
- **Pages**: Application pages.
- **Services**: Business logic of the application.
  
# Software:
- Visual Studio or any code editor compatible with JavaScript/TypeScript.
- Node.js and NPM (Node Package Manager).
- A modern web browser (Google Chrome, Mozilla Firefox, Microsoft Edge).

# Installation Instructions
1. **Download the ZIP file** containing the application and extract it to a folder.
2. **Open Visual Studio** and select **File > Open > Project/Solution**.
3. Choose the **HuffmanAlgorithm.sln** solution file located in the folder where you extracted the ZIP file.
4. Ensure the **Blazor WebAssembly project** is set as the startup project.

# Example Usage
1. Enter text in the input field on the homepage.
2. Click **Encode** to compress the data using the Huffman algorithm.
3. View the result in the **Encoded text** field.
4. To decompress the data, click **Decode**.

