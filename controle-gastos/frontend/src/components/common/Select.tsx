import React from 'react';

interface SelectOption {
  value: string | number;
  label: string;
}

interface SelectProps {
  label: string;
  name: string;
  options: SelectOption[];
  register: any;
  error?: string;
  placeholder?: string;
}

const Select: React.FC<SelectProps> = ({
  label,
  name,
  options,
  register,
  error,
  placeholder,
}) => {
  const containerStyle: React.CSSProperties = {
    marginBottom: '16px',
  };

  const labelStyle: React.CSSProperties = {
    display: 'block',
    marginBottom: '4px',
    fontSize: '14px',
    fontWeight: 600,
    color: '#374151',
  };

  const selectStyle: React.CSSProperties = {
    width: '100%',
    padding: '10px 12px',
    border: `1px solid ${error ? '#dc2626' : '#d1d5db'}`,
    borderRadius: '6px',
    fontSize: '14px',
    outline: 'none',
    backgroundColor: '#fff',
    boxSizing: 'border-box',
  };

  const errorStyle: React.CSSProperties = {
    color: '#dc2626',
    fontSize: '12px',
    marginTop: '4px',
  };

  return (
    <div style={containerStyle}>
      <label style={labelStyle} htmlFor={name}>
        {label}
      </label>
      <select
        id={name}
        name={name}
        style={selectStyle}
        {...register(name)}
      >
        {placeholder && (
          <option value="">{placeholder}</option>
        )}
        {options.map((option) => (
          <option key={option.value} value={option.value}>
            {option.label}
          </option>
        ))}
      </select>
      {error && <p style={errorStyle}>{error}</p>}
    </div>
  );
};

export default Select;
