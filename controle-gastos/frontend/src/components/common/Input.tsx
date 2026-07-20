import React from 'react';

interface InputProps {
  label: string;
  name: string;
  type?: string;
  register: any;
  error?: string;
  placeholder?: string;
}

const Input: React.FC<InputProps> = ({
  label,
  name,
  type = 'text',
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

  const inputStyle: React.CSSProperties = {
    width: '100%',
    padding: '10px 12px',
    border: `1px solid ${error ? '#dc2626' : '#d1d5db'}`,
    borderRadius: '6px',
    fontSize: '14px',
    outline: 'none',
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
      <input
        id={name}
        name={name}
        type={type}
        style={inputStyle}
        placeholder={placeholder}
        {...register(name)}
      />
      {error && <p style={errorStyle}>{error}</p>}
    </div>
  );
};

export default Input;
