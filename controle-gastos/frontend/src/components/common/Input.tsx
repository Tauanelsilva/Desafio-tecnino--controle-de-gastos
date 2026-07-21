import type { UseFormRegisterReturn } from 'react-hook-form';

interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
    label: string;
    register: UseFormRegisterReturn;
    error?: string;
}

export function Input({ label, register, error, type = 'text', ...props }: InputProps) {
    return (
        <div className="form-group">
            <label className="form-label">{label}</label>
            <input 
                className="form-input" 
                type={type} 
                {...register} 
                {...props} 
            />
            {error && <span className="form-error">{error}</span>}
        </div>
    );
}
