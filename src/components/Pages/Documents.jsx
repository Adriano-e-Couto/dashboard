import { useState } from "react";
import { Upload, FileText, CheckCircle } from "lucide-react";

function Documents() {
const [arquivo, setArquivo] = useState(null);

const handleFileChange = (event) => {
const file = event.target.files[0];


if (file) {
  setArquivo(file);
}


};

const handleUpload = () => {
if (!arquivo) {
return;
}


console.log("Arquivo selecionado:", arquivo);




};

return ( <div className="max-w-4xl mx-auto"> <div className="bg-white dark:bg-gray-800 rounded-2xl shadow-md p-8">
{/* Cabeçalho */} <div className="flex items-center gap-3 mb-8"> <FileText className="w-8 h-8 text-blue-600" />


      <div>
        <h1 className="text-2xl font-bold text-gray-800 dark:text-white">
          Documentos
        </h1>

        <p className="text-gray-500 dark:text-gray-400">
          Faça o upload dos seus arquivos
        </p>
      </div>
    </div>

    {/* Input escondido */}
    <input
      id="document-upload"
      type="file"
      className="hidden"
      onChange={handleFileChange}
      accept=".pdf,.doc,.docx,.xls,.xlsx,.jpg,.jpeg,.png"
    />

    {/* Área de upload */}
    <label
      htmlFor="document-upload"
      className="
        flex
        flex-col
        items-center
        justify-center
        w-full
        h-64
        border-2
        border-dashed
        border-slate-300
        dark:border-slate-600
        rounded-2xl
        cursor-pointer
        hover:border-blue-500
        hover:bg-blue-50
        dark:hover:bg-slate-700
        transition-all
        duration-300
      "
    >
      <Upload className="w-12 h-12 text-blue-500 mb-4" />

      <h2 className="text-lg font-semibold text-gray-700 dark:text-gray-200">
        Clique para selecionar um documento
      </h2>

      <p className="text-sm text-gray-500 mt-2">
        PDF, Word, Excel ou Imagem
      </p>
    </label>

    {/* Arquivo selecionado */}
    {arquivo && (
      <div className="mt-6 p-4 rounded-xl bg-green-50 dark:bg-green-900/20 border border-green-200 dark:border-green-800">
        <div className="flex items-center gap-3">
          <CheckCircle className="text-green-600 w-5 h-5" />

          <div>
            <p className="font-medium text-green-700 dark:text-green-300">
              Arquivo selecionado
            </p>

            <p className="text-sm text-gray-600 dark:text-gray-300">
              {arquivo.name}
            </p>
          </div>
        </div>
      </div>
    )}

    {/* Botão enviar */}
    <div className="mt-6">
      <button
        onClick={handleUpload}
        className="
          px-6
          py-3
          bg-blue-600
          hover:bg-blue-700
          text-white
          rounded-xl
          font-medium
          transition-all
          duration-200
          shadow-md
        "
      >
        Anexar Documento
      </button>
    </div>
  </div>
</div>


);
}

export default Documents;
