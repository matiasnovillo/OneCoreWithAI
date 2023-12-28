import sys
import nltk
from nltk.tokenize import sent_tokenize, word_tokenize
from nltk.corpus import stopwords
from nltk.probability import FreqDist

try:
    nltk.download('punkt')
    nltk.download('stopwords')

    # Verificar si se proporciona el texto como argumento al script
    if len(sys.argv) < 2:
        print("Por favor, proporciona el texto como argumento al script.")
    else:
        # Obtener el texto del argumento pasado
        texto = sys.argv[1]

        # Tokenizar el texto en oraciones
        oraciones = sent_tokenize(texto)

        # Tokenizar las palabras y eliminar stopwords
        palabras = word_tokenize(texto.lower())
        stop_words = set(stopwords.words('spanish'))
        palabras_filtradas = [palabra for palabra in palabras if palabra.isalnum() and palabra not in stop_words]

        # Calcular la frecuencia de las palabras
        frecuencia = FreqDist(palabras_filtradas)

        # Imprimir un resumen del texto
        print(" ".join(oraciones[:5]))  # Imprimir las primeras cinco oraciones como resumen

except Exception as e:
    print(f"Ocurrió un error: {e}")
