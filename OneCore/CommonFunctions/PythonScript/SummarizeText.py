import sys
from sumy.summarizers.text_rank import TextRankSummarizer
from sumy.parsers.plaintext import PlaintextParser
from sumy.nlp.tokenizers import Tokenizer

def copiar_contenido_archivo():
    try:    
        # Verificar si se proporcionaron los argumentos esperados
        if len(sys.argv) < 2:
            print("No se especificaron argumentos de entrada")
            return
        
        textToSummarize = sys.argv[1]
            
        # Crear un resumen usando TextRank
        parser = PlaintextParser.from_string(textToSummarize, Tokenizer('spanish'))
        summarizer = TextRankSummarizer()
        summary = summarizer(parser.document, 5)  # Obtener un resumen de 5 oraciones
        
        # Convertir el resumen a texto
        summary_text = ' '.join(map(str, summary))
        
        print(summary_text)
    except Exception as e:
        print(f"Error: {e}")


copiar_contenido_archivo()
