interface FormMessageProps {
  erro: string | null;
  sucesso?: string | null;
}

export default function FormMessage({ erro, sucesso }: FormMessageProps) {
  if (!erro && !sucesso) return null;

  return (
    <div className="mt-2 text-sm">
      {erro && <p className="text-red-600">{erro}</p>}
      {sucesso && <p className="text-green-600">{sucesso}</p>}
    </div>
  );
}