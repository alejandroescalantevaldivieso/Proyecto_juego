## 📌 Guía de instalación del repositorio

Esta guía explica paso a paso cómo instalar y configurar el entorno necesario para poder clonar, abrir y ejecutar correctamente este proyecto en Unity.

---

## 🔗 Enlaces de descarga (IMPORTANTE)

Antes de iniciar la instalación, descarga e instala los siguientes programas:

- Unity Hub:
  - 👉 [https://cloud.unity.com/home/organizations/4674140173068/onboarding/post-download?locale=es&code=uUtRtRnyPFeIp898LGPdeQ004f&locale=es&session_state=839f536b7cc2b515fbda457254094f4c8783a8d3b5d64c82cffa24cdd6f86ec9.kE5U5K5Dss56JatfDPheyw001f]

- Unity 2022.3.62f3 LTS:
  - 👉 [https://unity.com/es/releases/editor/archive]

- Git:
  - 👉 [https://git-scm.com/]

---

## 🧱 1. Instalación de Unity

1. Instalar **Unity Hub**
2. Abrir Unity Hub
3. Ir a **Installs**
4. Seleccionar **Install Editor**
5. Instalar la versión:
   - **Unity 2022.3.62f3 LTS**

⚠️ Importante:
- No usar otra versión distinta
- No actualizar el proyecto a versiones más nuevas

---

## 🧰 2. Instalación de Git Bash

1. Instalar Git desde el enlace oficial
2. Durante la instalación asegurarse de incluir:
   - Git Bash
   - Git Credential Manager (recomendado)

---

## 👤 3. Configuración inicial de Git (credenciales)

Abrir **Git Bash** y ejecutar:

```bash
git config --global user.name "Tu Nombre"
git config --global user.email "tu_correo@ejemplo.com"

---

## 📦 4. Instalación de Git LFS

Git LFS es obligatorio para manejar archivos grandes.

Instalar:
git lfs install

Verificar:
git lfs version

Si devuelve una versión, está correcto.

---

## 📁 5. Clonar el repositorio (forma recomendada)

Paso 1: Crear carpeta (ej: ProyectosUnity)  
Paso 2: Click derecho → Git Bash Here  
Paso 3: Ejecutar:

git clone https://URL-DEL-REPOSITORIO.git

---

## ⏳ 6. Después de clonar

Una vez clonado el proyecto:

1. Esperar a que Git LFS descargue todos los archivos.
2. **No abrir Unity** hasta que la descarga termine completamente.
3. Verificar que no haya errores en la terminal.

---

## 🎮 7. Abrir el proyecto en Unity

1. Abrir **Unity Hub**.
2. Hacer clic en **Add / Agregar**.
3. Seleccionar la carpeta del proyecto clonado.
4. Abrir con la versión: **Unity 2022.3.62f3 LTS**.
5. Esperar la importación inicial del proyecto.






