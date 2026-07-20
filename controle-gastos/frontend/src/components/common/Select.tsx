import { UseFormRegisterReturn } from 'react-hook-form';

interface SelectOption {
    label: string;
    value: string | number;
}

interface SelectProps extends React.SelectHTMLAttributes<HTMLSelectElement> {
    label: string;
    options: SelectOption[];
    register: UseFormRegisterReturn;
    error?: string;
}

export function Select({ label, options, register, error, ...props }: SelectProps) {
    return (
        <div className="form-group">
            <label className="form-label">{label}</label>
            <select className="form-select" {...register} {...props}>
                <option value="">Selecione...</option>
                {options.map(opt => (
                    <option key={opt.value} value={opt.value}>
                        {opt.label}
                    </option>
                ))}
            </select>
            {error && <span className="form-error">{error}</span>}
        </div>
    );
}
