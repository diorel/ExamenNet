# Payment Request — Guia de Ejecucion

---

## Backend — PaymentAPI (ASP.NET Core 8)

### Requisitos del sistema

| Requisito | Version | Verificar |
|-----------|---------|-----------|
| Sistema Operativo | Windows 10/11 | — |
| .NET SDK | 8.0 | `dotnet --version` |
| SQL Server Express | Cualquier version activa | SSMS o `sqlcmd` |

### Paquetes instalados (NuGet)

| Paquete | Version |
|---------|---------|
| EntityFrameworkCore.SqlServer | 8.0.0 |
| EntityFrameworkCore.Tools | 8.0.0 |
| EntityFrameworkCore.Design | 8.0.0 |
| Swashbuckle.AspNetCore (Swagger) | 6.5.0 |

### Comandos para levantar

```bash
cd ExamenNet/Backend/PaymentAPI

dotnet restore                        # Restaurar dependencias
dotnet ef database update             # Crear tabla en SQL Server
dotnet run                            # Iniciar la API
```

**Datos de conexion:**

| Parametro | Valor |
|-----------|-------|
| Servidor | `LAPTOP-HGP68D9D\SQLEXPRESS` |
| Base de datos | `Pagos` |
| Login | `admin` |
| Password | `12345678` |

**Comandos a ejecutar en orden:**

| Orden | Comando | Descripcion |
|-------|---------|-------------|
| 1 | `dotnet ef migrations add InitialCreate` | Genera los archivos de migracion en la carpeta `Migrations/` |
| 2 | `dotnet ef database update` | Se conecta a SQL Server y crea la tabla `PaymentRequests` en la base de datos `Pagos` |


### URLs disponibles

| Recurso | URL |
|---------|-----|
| API Base | `http://localhost:5102` |
| Swagger UI | `http://localhost:5102/swagger` |
| GET solicitudes | `http://localhost:5102/api/payment-requests` |
| POST solicitudes | `http://localhost:5102/api/payment-requests` |

### Arquitectura Backend

```
HTTP Request
    ↓
GlobalExceptionMiddleware   ← captura errores, retorna JSON
    ↓
Controller                  ← recibe y responde HTTP (sin logica)
    ↓
Service                     ← valida reglas de negocio, mapea datos
    ↓
Repository                  ← accede a la base de datos
    ↓
AppDbContext (EF Core)
    ↓
SQL Server — tabla PaymentRequests
```

---

## Frontend — payment-frontend (Angular 16.2)

### Requisitos del sistema

| Requisito | Version | Verificar |
|-----------|---------|-----------|
| Sistema Operativo | Windows 10/11 | — |
| Node.js | 18 LTS o superior | `node -v` |
| npm | 9 o superior | `npm -v` |
| Angular CLI | 16.2.0 | `ng version` |

> Instalar Angular CLI si no esta: `npm install -g @angular/cli@16.2.0`

### Dependencias principales (package.json)

| Paquete | Version |
|---------|---------|
| @angular/core | ^16.2.0 |
| @angular/forms (Reactive Forms) | ^16.2.0 |
| @angular/common/http (HttpClient) | ^16.2.0 |
| rxjs | ~7.8.0 |
| TypeScript | ~5.1.3 |

### Comandos para levantar

```bash
cd ExamenNet/Frontend/payment-frontend

npm install     # Instalar dependencias
ng serve        # Iniciar la aplicacion
```

### URLs disponibles

| Recurso | URL |
|---------|-----|
| Aplicacion | `http://localhost:4200` |
| API consumida | `http://localhost:5102/api` |

> El backend debe estar corriendo antes de usar el frontend.

### Arquitectura Frontend

```
AppComponent
├── PaymentFormComponent        ← formulario, validaciones, estados UI
│       ↓ (evento al crear)
└── PaymentListComponent        ← tabla de solicitudes, recarga automatica
        ↓
  PaymentRequestService         ← unico punto de comunicacion HTTP
        ↓
  GET / POST → http://localhost:5102/api/payment-requests
```

---

*Version 1.0 — 2026-05-08*
