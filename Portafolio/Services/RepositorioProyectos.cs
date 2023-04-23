namespace Portafolio.Models
{
    public class RepositorioProyectos
    {
        public List<Proyecto> ObtenerProyectos()
        {
            return new List<Proyecto>()
            {
                new Proyecto {
                    Titulo = "Kioscos",
                    Descripcion = "Proyecto académico realizado para la clase de Industria del software, en la carrera de Ingeniería en sistemas de la Universidad Nacional Autónoma de Honduras.\nKioscos APP, una mejora al proyecto de QuioscosAPP. Este es un proyecto full stack elaborado bajo las tecnologías de React, MySQL, Prisma ORM, Node, Express, Tailwindcss.\nKiosco es una plataforma para facilitar los pedidos de los restaurantes. Nuestra plataforma ayuda para evitar las filas al momento de pedir órdenes y poder agilizar el proceso, el uso de Kiosco es simple, ya que incluye login para los clientes que quieran consumir en los restaurantes y para los mismos restaurantes con el control absoluto de su menú y las órdenes pendientes/completadas y poder dar un mejor servicio.",
                    ImagenURL = "https://res.cloudinary.com/dicifr3km/image/upload/v1673994368/large_Kioscos_96dc69a033.png",
                    Link = "https://kioscos.vercel.app/"
                },
                new Proyecto {
                    Titulo = "Uptasks",
                    Descripcion = "Proyecto académico desarrollado del curso React - La Guía Completa: Hooks Context Redux MERN +15 Apps. Proyecto MERN Stack con Nodejs, express, MongoDB, socket.io,  TailwindCSS, HeadlessUI, Context API. \nUptasks es administrador de proyectos donde los usuarios pueden registrar sus propias cuentas, cambiar sus contraseñas, confirmar sus cuentas, crear proyectos, asignar tareas, agregar colaboradores, marcar proyectos como finalizados, eliminar proyectos, entre otros.\n",
                    ImagenURL = "https://res.cloudinary.com/dicifr3km/image/upload/v1673994669/large_Uptasks_368208719d.png",
                    Link = "https://uptasks.vercel.app/"
                },
                new Proyecto {
                    Titulo = "GuitarLA",
                    Descripcion = "Proyecto académico desarrollado del curso React - La Guía Completa: Hooks Context Redux MERN +15 Apps. Proyecto realizado con Nextjs y strapi CMS, utilizando postgresql para la base de datos.\nGuitarLA es una tienda de instrumentos musicales, blog, carrito de compras y cursos. El fronend está elaborado con nextjs y su backend está apoyado en Strapi CMS que cuenta con un panel de administración para la gestión de sus datos.\n",
                    ImagenURL = "https://res.cloudinary.com/dicifr3km/image/upload/v1673994927/large_Guitar_LA_5edd3a7956.png",
                    Link = "https://nextguitarla.vercel.app/"
                },
                new Proyecto {
                    Titulo = "EduEvents",
                    Descripcion = "Proyecto académico realizado para la clase de Ingeniería del software, en la carrera de Ingeniería en sistemas de la Universidad Nacional Autónoma de Honduras.\nEduEvents es un sistema para la gestión de eventos académicos, realizado bajo las tecnologías de Angular, Nodejs, express, MySQL.\nEduEvents es una plataforma para personas que buscan gestionar sus eventos o para personas que quieren asistir a ellos. Ofrecer eventos, conferencias y talleres para facilitar la organización de eventos académicos, virtuales o presenciales.\nEs una plataforma que siempre está disponible al servicio de sus usuarios, poniéndola al alcance del público en general y expertos que imparten temáticas.",
                    ImagenURL = "https://res.cloudinary.com/dicifr3km/image/upload/v1673995184/large_Edu_Events_5fe8831988.png",
                    Link = "https://eduevents.vercel.app/"
                },
                new Proyecto {
                    Titulo = "Lienzo",
                    Descripcion = "Proyecto académico realizado para la clase de Bases de datos I, en la carrera de Ingeniería en sistemas de la Universidad Nacional Autónoma de Honduras.\nPrograma en Python3 que hace uso de MariaDB/MySQL (en compatibilidad) para el almacenamiento, modificación, eliminación y recuperación de datos.\nEl proyecto fue desarrollado utilizando Python3, PyQT5, Tkinter, MySQL.\nUtilizamos el diseñador de ventanas de qt5 para desarrollar de manera rápida los scripts de python para generar la interfaz gráfica.\n",
                    ImagenURL = "https://res.cloudinary.com/dicifr3km/image/upload/v1673995504/large_Lienzo_ee790e692d.png",
                    Link = "https://www.youtube.com/watch?v=ik6TLqDWmLw"
                },
                new Proyecto {
                    Titulo = "Control de presupuesto",
                    Descripcion = "Proyecto académico desarrollado del curso React - La Guía Completa: Hooks Context Redux MERN +15 Apps.\nAplicación web realizada con Reactjs que permite llevar un control de su presupuesto. La aplicación también será capaz de clasificar por categoría cada gasto que vaya realizando, además podrá ver de manera interactiva cuánto queda de su presupuesto.\n",
                    ImagenURL = "https://res.cloudinary.com/dicifr3km/image/upload/v1673995840/large_Ctrl_40352f2d6c.png",
                    Link = "https://ctrlpresupuesto.netlify.app"
                },
                new Proyecto {
                    Titulo = "Prueba de penetración",
                    Descripcion = "Proyecto académico realizado para la clase de Seguridad Informática, en la carrera de Ingeniería en sistemas de la Universidad Nacional Autónoma de Honduras.\nEn este laboratorio, aprenderemos cómo hackear un dispositivo móvil Android y un computador con windows usando MSFvenom y el marco Metasploit . Usaremos MSFvenom para generar la carga útil, lo guardaremos como un archivo .apk y .exe, además configuraremos un oyente para el framework de Metasploit. Una vez que el usuario / víctima descarga e instala el .apk/.exe malicioso, un atacante puede recuperar fácilmente la sesión en Metasploit. Para lograr esto, un atacante necesita hacer un poco de ingeniería social para instalar el .apk en el dispositivo móvil de la víctima.\n",
                    ImagenURL = "https://res.cloudinary.com/dicifr3km/image/upload/v1673995701/large_Kali_cf1aca83db.jpg",
                    Link = "https://www.youtube.com/watch?v=rjxT27mOO2o"
                },
                new Proyecto {
                    Titulo = "Quioscoapp",
                    Descripcion = "Proyecto académico desarrollado del curso React - La Guía Completa: Hooks Context Redux MERN +15 Apps. Proyecto full stack utilizando tecnologías como Nextjs, Tailwindcss, Prisma ORM, SWR.\nQuioscosAPP es un quiosco de comida mediante el cual se puede seleccionar el menú de manera virtual, además la app se conecta directamente con la cocina para que ellos puedan atender tu pedido de manera ágil.",
                    ImagenURL = "https://res.cloudinary.com/dicifr3km/image/upload/v1673993810/large_quioscos_5893a2cec5.png",
                    Link = "https://quioscoapp.vercel.app"
                },
                new Proyecto {
                    Titulo = "React Node Send",
                    Descripcion = "Proyecto académico desarrollado del curso React - La Guía Completa: Hooks Context Redux MERN +15 Apps. \nReact  Node Send es un proyecto realizado con Nextjs, Nodejs, Express y MongoDB.\nNodeSend es una copia de Firefox send, permite la subida de archivos a un servidor ademas, permite compartir enlaces de descargas. Si un usuario esta registrado en la plataforma, nodesend permite establecer reglas en los enlaces de descarga, por ejemplo, el limite de descargas, colocar contraseñas a tus archivos, etc.\n",
                    ImagenURL = "https://res.cloudinary.com/dicifr3km/image/upload/v1675286180/large_2023_02_01_15_15_2db7872f07.png",
                    Link = "https://rnsend.netlify.app/"
                },

            };
        }
    }
}