using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniValidation;
using Proyectoprogra5.DataAccess.Models;
using System.Net.Mail;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProyectoProgra5Context>(options =>
{
    options.UseSqlServer("name=ConnectionStrings:DefaultConnection");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
    .WithName("GetWeatherForecast");
//Mantenimiento Servidores
app.MapPost("/Servidor", async ([FromBody] Servidor serv, ProyectoProgra5Context context) =>
{
    try
    {
        if (!MiniValidator.TryValidate(serv, out var errors))
        {
            return Results.BadRequest(new { codigo = -2, mensaje = StatusCodes.Status404NotFound, errores = errors });
        }
        await context.Servidors.AddAsync(serv);
        await context.SaveChangesAsync();
        return Results.Created($"/Servidor/{serv.Codigo}",
 new
 {
     codigo = 0,
     mensaje = StatusCodes.Status201Created,
     serv = serv,
     //nombreEstado = cliente.EstadoNavigation!.Nombre
 });
    }
    catch (Exception exc)
    {
        return Results.Json(new
        {
            codigo = -1,
            mensaje = StatusCodes.Status409Conflict
        },
        statusCode: StatusCodes.Status409Conflict);

    }

});
app.MapGet("Servidor/{Nombre}", async (string Nombre, ProyectoProgra5Context context) =>
{
    if (await context.Servidors.AnyAsync<Servidor>(x => x.Nombre == Nombre))
    {
        return Results.Ok(StatusCodes.Status200OK);
    }
    return Results.NotFound();
});
app.MapPost("/Servidor/{Nombre}/{Contraseña}", async (Servidor serv, ProyectoProgra5Context context, string id, string contraseña) =>
{
    try
    {
        if (!MiniValidator.TryValidate(serv, out var errors))
        {
            return Results.BadRequest(new { codigo = -2, mensaje = StatusCodes.Status404NotFound, errores = errors });
        }

        await context.Servidors.AnyAsync<Servidor>(S => S.Administrador == id && S.Contraseña == contraseña);
        await context.SaveChangesAsync();
        //context.Entry(para).Reference(c => c.EstadoNavigation).Load();
        return Results.Ok(
 new
 {
     mensaje = StatusCodes.Status200OK,
 });
    }
    catch (Exception exc)
    {
        return Results.Json(new
        {
            codigo = -1,
            mensaje = StatusCodes.Status409Conflict
        },
        statusCode: StatusCodes.Status409Conflict);

    }

});

app.MapPut("/Servidor/{id}", async (int id, Servidor serv, ProyectoProgra5Context context) =>
{
    var todo = await context.Servidors.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Nombre = serv.Nombre;
    todo.Descripcion = serv.Descripcion;
    todo.Contraseña = serv.Contraseña;
    todo.Administrador = serv.Administrador;

    await context.SaveChangesAsync();

    return Results.NoContent();
});
app.MapDelete("/Servidor/{id}", async (int id, ProyectoProgra5Context context) =>
{
    if (await context.Servidors.FindAsync(id) is Servidor todo)
    {
        context.Servidors.Remove(todo);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});
//Parametros
app.MapPost("/ParametrosServidore", async ([FromBody] ParametrosServidore serv, ProyectoProgra5Context context) =>
{
    try
    {
        if (!MiniValidator.TryValidate(serv, out var errors))
        {
            return Results.BadRequest(new { codigo = -2, mensaje = StatusCodes.Status404NotFound, errores = errors });
        }
        await context.ParametrosServidores.AddAsync(serv);
        await context.SaveChangesAsync();
        return Results.Created($"/Servidor/{serv.IdServidor}",
        new
        {
            codigo = 0,
            mensaje = StatusCodes.Status201Created,
            serv = serv,
            //nombreEstado = cliente.EstadoNavigation!.Nombre
        });
    }
    catch (Exception exc)
    {
        throw new Exception(exc.InnerException.ToString());
        return Results.Json(new { codigo = -1, mensaje = exc.Message },
        statusCode: StatusCodes.Status500InternalServerError);
    }
    //catch (Exception exc)
    //{
    //    return Results.Json(new
    //    {
    //        codigo = -1,
    //        mensaje = StatusCodes.Status409Conflict
    //    },
    //    statusCode: StatusCodes);

    //}

});
app.MapGet("ParametrosServidore/{IdServidor}", async (int Servidores, ProyectoProgra5Context context) =>
{
    if (await context.ParametrosServidores.AnyAsync<ParametrosServidore>(x => x.IdServidor == Servidores))
    {
        return Results.Ok(StatusCodes.Status200OK);
    }
    return Results.NotFound();
}); app.MapPut("/ParametrosServidore/{IdServidor}", async (int ID, ParametrosServidore para, ProyectoProgra5Context context) =>
{
    var seleccionar = await context.ParametrosServidores.FindAsync(ID); if (seleccionar is null) return Results.NotFound(); seleccionar.Porcentaje = para.Porcentaje;
    await context.SaveChangesAsync(); return Results.NoContent();
}); app.MapDelete("/ParametrosServidore/{IdParametro}", async (int IdParametro, ProyectoProgra5Context context) =>
{
    if (await context.ParametrosServidores.FindAsync(IdParametro) is ParametrosServidore borra)
    {
        context.ParametrosServidores.Remove(borra);
        await context.SaveChangesAsync();
        return Results.Ok(borra);
    }
    return Results.NotFound();
});
//Mantenimiento Servicios
app.MapPost("/Servicio", async ([FromBody] Servicio para, ProyectoProgra5Context context) =>
{
    try
    {
        if (!MiniValidator.TryValidate(para, out var errors))
        {
            return Results.BadRequest(new { codigo = -2, mensaje = StatusCodes.Status404NotFound, errores = errors });
        }
        await context.Servicios.AddAsync(para);
        await context.SaveChangesAsync();
        //context.Entry(para).Reference(c => c.EstadoNavigation).Load();
        return Results.Created($"/Servicio/{para.Codigo}",
 new
 {
     codigo = 0,
     mensaje = StatusCodes.Status201Created,
     para = para,
 });
    }
    catch (Exception exc)
    {
        return Results.Json(new
        {
            codigo = -1,
            mensaje = exc.Message
        },
    statusCode: StatusCodes.Status500InternalServerError);
    }
});
app.MapGet("Servicio/{CodigoServidor}", async (int Codigo, ProyectoProgra5Context context) =>
{
    if (await context.Servicios.AnyAsync<Servicio>(x => x.CodigoServidor == Codigo))
    {
        return Results.Ok(StatusCodes.Status200OK);
    }
    return Results.NotFound();
});
app.MapPut("/Servicio/{id}", async (int id, Servicio servi, ProyectoProgra5Context context) =>
{
    var todo = await context.Servicios.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Nombre = servi.Nombre;
    todo.Descripcion = servi.Descripcion;
    todo.CodigoServidor = servi.CodigoServidor;
    todo.Timeout = servi.Timeout;

    await context.SaveChangesAsync();

    return Results.NoContent();
});
app.MapDelete("/Servicio/{id}", async (int id, ProyectoProgra5Context context) =>
{
    if (await context.Servicios.FindAsync(id) is Servicio todo)
    {
        context.Servicios.Remove(todo);
        await context.SaveChangesAsync();
        return Results.Ok(todo);
    }

    return Results.NotFound();
});

//Email
app.MapPost("/emails", async (string asuntoCorreo, string cuerpoCorreo, string[] listaDestinatarios, ProyectoProgra5Context context) =>
{
    try
    {
        SmtpClient smtp = new SmtpClient()
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential()
            {
                UserName = "daiberfranciscocontreras@gmail.com",
                Password = "qgszgoessllkcjaa"
            }
        };
        MailAddress from = new MailAddress("daiberfranciscocontreras@gmail.com");
        MailMessage mensaje = new MailMessage()
        {
            From = from,
            Subject = asuntoCorreo,
            Body = cuerpoCorreo,
        };
        foreach (string u in listaDestinatarios)
        {
            if (await context.Usuarios.AnyAsync(x => x.Usuario1 == u))
            {

                if (await context.EncargadoServidores.AnyAsync(x => x.Alerta == true))
                {
                    Usuario user = await context.Usuarios.FirstOrDefaultAsync(x => x.Usuario1 == u);
                    MailAddress to = new MailAddress(user.Correo);
                    mensaje.To.Add(to);
                }
            }
        }
        smtp.Send(mensaje); return Results.Ok(new { mensaje ="Correo Enviado" });
    }
    catch (Exception exc)
    {
        return Results.Json(new { codigo = -1, mensaje = exc.Message },
        statusCode: StatusCodes.Status500InternalServerError);
    }
});

//Iniciar/Detener Monitoreo
app.MapPut("/ManejarMonitoreo", async (int codigo, string Usuario, short tipoAlerta, ProyectoProgra5Context context) =>
{
    try
    {
        if (await context.EncargadoServicios.AnyAsync(x => x.CodigoServicio == codigo))
        {
            var user = await context.EncargadoServicios.FindAsync(Usuario);
            user.Alerta = tipoAlerta == 1 ? true : false;
            context.EncargadoServicios.Update(user);
            await context.SaveChangesAsync();
            return Results.NoContent();
        }
        else if (await context.EncargadoServidores.AnyAsync(x => x.CodigoServidor == codigo))
        {
            var user = await context.EncargadoServidores.FindAsync(Usuario);
            user.Alerta = tipoAlerta == 1 ? true : false;
            context.EncargadoServidores.Update(user);
            await context.SaveChangesAsync();
            return Results.NoContent();
        }
        return Results.NotFound("No existe el código del parámetro ingresado.");
    }
    catch (Exception exc)
    {
        return Results.Json(new { codigo = -1, mensaje = exc.Message },
        statusCode: StatusCodes.Status500InternalServerError);
    }
});

// Usuarios
app.MapPost("/usuarios", async ([FromBody] Usuario usu, ProyectoProgra5Context context) =>
{
    try
    {
        if (!MiniValidator.TryValidate(usu, out var errors))
        {
            return Results.BadRequest(new { codigo = -2, mensaje = StatusCodes.Status404NotFound, errores = errors });
        }
        await context.Usuarios.AddAsync(usu);
        await context.SaveChangesAsync();
        //context.Entry(para).Reference(c => c.EstadoNavigation).Load();
        return Results.Created($"/Servicio/{usu.Usuario1}",
 new
 {
     codigo = 0,
     mensaje = StatusCodes.Status201Created,
     para = usu,
 });
    }
    catch (Exception exc)
    {
        return Results.Json(new
        {
            codigo = -1,
            mensaje = exc.Message
        },
    statusCode: StatusCodes.Status500InternalServerError);
    }
});

// Encargado Servidores
app.MapPost("/EncargadoServidores", async ([FromBody] EncargadoServidore Enser, ProyectoProgra5Context context) =>
{
    try
    {
        if (!MiniValidator.TryValidate(Enser, out var errors))
        {
            return Results.BadRequest(new { codigo = -2, mensaje = StatusCodes.Status404NotFound, errores = errors });
        }
        await context.EncargadoServidores.AddAsync(Enser);
        await context.SaveChangesAsync();
        //context.Entry(para).Reference(c => c.EstadoNavigation).Load();
        return Results.Created($"/Servicio/{Enser.Usuario}",
 new
 {
     codigo = 0,
     mensaje = StatusCodes.Status201Created,
     para = Enser,
 });
    }
    catch (Exception exc)
    {
        return Results.Json(new
        {
            codigo = -1,
            mensaje = exc.Message
        },
    statusCode: StatusCodes.Status500InternalServerError);
    }
});
app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}