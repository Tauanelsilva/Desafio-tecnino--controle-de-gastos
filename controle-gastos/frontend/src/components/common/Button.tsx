import React from 'react';

interface ButtonProps {
  children: React.ReactNode;
  type?: 'submit' | 'button';
  variant?: 'primary' | 'danger' | 'secondary';
  onClick?: () => void;
  disabled?: boolean;
}

const Button: React.FC<ButtonProps> = ({
  children,
  type = 'button',
  variant = 'primary',
  onClick,
  disabled = false,
}) => {
  const baseStyle: React.CSSProperties = {
    padding: '10px 20px',
    border: 'none',
    borderRadius: '6px',
    cursor: disabled ? 'not-allowed' : 'pointer',
    fontSize: '14px',
    fontWeight: 600,
    transition: 'opacity 0.2s',
    opacity: disabled ? 0.6 : 1,
  };

  const variants: Record<string, React.CSSProperties> = {
    primary: { backgroundColor: '#2563eb', color: '#fff' },
    danger: { backgroundColor: '#dc2626', color: '#fff' },
    secondary: { backgroundColor: '#6b7280', color: '#fff' },
  };

  return (
    <button
      type={type}
      style={{ ...baseStyle, ...variants[variant] }}
      onClick={onClick}
      disabled={disabled}
    >
      {children}
    </button>
  );
};

export default Button;
