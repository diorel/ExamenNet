ok# Payment Request — Guia de Ejecucion

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

## Configuración de la Cadena de Conexión SQL Server

Antes de ejecutar el proyecto es necesario actualizar las credenciales de conexión a SQL Server en el archivo:

```text
appsettings.json
```

Actualmente el proyecto incluye una configuración de ejemplo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=LAPTOP-HGP68D9D\\SQLEXPRESS;Database=Pagos;User Id=admin;Password=12345678;TrustServerCertificate=True;MultipleActiveResultSets=true"
}
```

Cada usuario debe reemplazar estos valores por las credenciales correspondientes a su propio entorno local de SQL Server.

## Datos que deben modificarse

| Campo | Descripción |
|---|---|
| Server | Nombre de la instancia local de SQL Server |
| Database | Nombre de la base de datos |
| User Id | Usuario configurado en SQL Server |
| Password | Contraseña del usuario SQL |

---

## Ejemplo de configuración personalizada

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=MI_SERVIDOR\\SQLEXPRESS;Database=Pagos;User Id=miUsuario;Password=miPassword;TrustServerCertificate=True;MultipleActiveResultSets=true"
}
```

---

## Ejemplo del nombre del servidor local

Para identificar el nombre de tu servidor SQL Server puedes abrir:

- SQL Server Management Studio (SSMS)

y verificar el nombre de la instancia, por ejemplo:

```text
LAPTOP-HGP68D9D\SQLEXPRESS
```

---

## Importante

Asegúrate de que:

- SQL Server esté en ejecución.
- El usuario tenga permisos sobre la base de datos.
- El modo de autenticación SQL Server esté habilitado si utilizas `User Id` y `Password`.
- La base de datos `Pagos` exista o se cree mediante las migraciones de Entity Framework Core.


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
