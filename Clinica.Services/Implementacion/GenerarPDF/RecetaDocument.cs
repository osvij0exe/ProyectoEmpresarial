using AutoMapper;
using Clinica.Models.Response;
using Clinica.Repositories.Implementacion;
using Clinica.Services.Interfaces.GenerarPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Implementacion.GenerarPDF
{
    public class RecetaDocument : IDocumento
    {
        private readonly RecetarioDTOResponse _response;

        public RecetaDocument(RecetarioDTOResponse response)
        {

            _response = response;
        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;


        public void Compose(IDocumentContainer container , int id)
        {
            container.Page(pagina =>
            {
                pagina.Margin(30);

                pagina.Header();
                pagina.Content().Element(ComposeContent);
                pagina.Footer();

            });
        }
        


        void ComposeContent(IContainer container)
        {
            //var response = _response.FindByIdAsync(Id);


            container.PaddingVertical(0).Column(elementos =>
            {
                elementos.Spacing(25);

                foreach (var i in Enumerable.Range(0, 2))
                {


                    elementos.Item().PaddingTop(0).Column(columna =>
                    {
                        columna.Item().Background(Colors.LightBlue.Lighten5).Row(fila =>
                        {

                            fila.ConstantItem(140).Height(60).Placeholder();


                            if (_response.Medico.Genero.Equals('F'))
                            {
                                fila.RelativeItem().Column(col =>
                                {
                                    //generar columnas hacia abajo
                                    col.Item().PaddingLeft(5).Text($"Dra.{_response.Medico.Nombres} {_response.Medico.Apellidos}")
                                    .Bold()
                                    .FontColor(Colors.Blue.Darken3)
                                    .FontSize(13);

                                    col.Item().PaddingLeft(5).Text($"Médico Epecialista en {_response.Medico.Especialidad}")
                                    .Bold()
                                    .FontColor(Colors.Blue.Darken1)
                                    .FontSize(12);
                                });

                            }
                            else
                            {
                                fila.RelativeItem().Column(col =>
                                {
                                    //generar columnas hacia abajo
                                    col.Item().PaddingLeft(5).Text($"Dr.{_response.Medico.Nombres} {_response.Medico.Apellidos}")
                                    .Bold()
                                    .FontColor(Colors.Blue.Darken3)
                                    .FontSize(13);

                                    col.Item().PaddingLeft(5).Text($"Médico Epecialista en {_response.Medico.Especialidad}")
                                    .Bold()
                                    .FontColor(Colors.Blue.Darken1)
                                    .FontSize(12);
                                });
                            }



                            fila.RelativeItem().Column(col =>
                            {
                                col.Item().PaddingRight(10).AlignRight().Text($"Cedula Porfecional: {_response.Medico.CedulaProfecional}")
                                .FontColor(Colors.Blue.Accent3)
                                .FontSize(11);

                                col.Item().PaddingRight(10).AlignRight().Text("Medica Sur")
                                .FontColor(Colors.Blue.Accent3)
                                .FontSize(11);
                            });
                        });

                        columna.Item().LineHorizontal(1.8f).LineColor(Colors.Blue.Darken4);

                        columna.Item().Height(20).Text(txt =>
                        {
                            txt.Span("Datos del Paciente")
                            .FontColor(Colors.Blue.Darken4)
                            .Underline()
                            .Bold();
                        });

                        columna.Item().Height(30).Row(fila =>
                        {
                            fila.RelativeItem().PaddingLeft(5).Text(txt =>
                            {
                                txt.Span($"Nombre: {_response.Paciente.Nombres} ")
                                .FontColor(Colors.Blue.Darken4)
                                .SemiBold()
                                .FontSize(10);
                            });

                            fila.RelativeItem().PaddingLeft(5).Text(txt =>
                            {
                                txt.Span($"Apellidos: {_response.Paciente.Apellidos}")
                                .FontColor(Colors.Blue.Darken4)
                                .SemiBold()
                                .FontSize(10);
                            });

                            fila.RelativeItem().PaddingLeft(5).Text(txt =>
                            {
                                txt.Span($"Fecha: {_response.FechaCreacion}")
                                .FontColor(Colors.Blue.Darken4)
                                .SemiBold()
                                .FontSize(10);
                            });
                        });

                        columna.Item().Height(200).Row(fila =>
                        {
                            fila.ConstantItem(100).Column(col =>
                            {
                                col.Item().Padding(2).Text(txt =>
                                {
                                    txt.Span($"Edad: {_response.Paciente.Edad} ")
                                    .FontColor(Colors.Blue.Darken4)
                                    .SemiBold()
                                    .FontSize(9);
                                });
                                col.Item().Padding(2).Text(txt =>
                                {
                                    txt.Span($"TA: {_response.Paciente.TensionArterial}")
                                    .FontColor(Colors.Blue.Darken4)
                                    .SemiBold()
                                    .FontSize(9);
                                });
                                col.Item().Padding(2).Text(txt =>
                                {
                                    txt.Span($"FC: {_response.Paciente.FrecuenciaCardiaca}")
                                    .FontColor(Colors.Blue.Darken4)
                                    .SemiBold()
                                    .FontSize(9);
                                });
                                col.Item().Padding(2).Text(txt =>
                                {
                                    txt.Span($"F.R: {_response.Paciente.FactorReumatoideo}")
                                    .FontColor(Colors.Blue.Darken4)
                                    .SemiBold()
                                    .FontSize(9);
                                });
                                col.Item().Padding(2).Text(txt =>
                                {
                                    txt.Span($"TEMP: {_response.Paciente.Temperatura}")
                                    .FontColor(Colors.Blue.Darken4)
                                    .SemiBold()
                                    .FontSize(9);
                                });
                                col.Item().Padding(2).Text(txt =>
                                {
                                    txt.Span($"PESO: {_response.Paciente.Peso}")
                                    .FontColor(Colors.Blue.Darken4)
                                    .SemiBold()
                                    .FontSize(9);
                                });
                                col.Item().Padding(2).Text(txt =>
                                {
                                    txt.Span($"TALLA: {_response.Paciente.Talla}")
                                    .FontColor(Colors.Blue.Darken4)
                                    .SemiBold()
                                    .FontSize(9);
                                });
                                col.Item().Padding(2).Text(txt =>
                                {
                                    txt.Span($"IMC: {_response.Paciente.IMC}")
                                    .FontColor(Colors.Blue.Darken4)
                                    .SemiBold()
                                    .FontSize(9);
                                });
                                col.Item().Padding(2).Text(txt =>
                                {
                                    txt.Span($"ID: {_response.Paciente}")
                                    .FontColor(Colors.Blue.Darken4)
                                    .SemiBold()
                                    .FontSize(9);
                                });
                            });


                            fila.RelativeItem().Text(txt =>
                            {
                                txt.ParagraphSpacing(5);
                                txt.Line("Prescripción: ")
                                .FontColor(Colors.Blue.Darken4);
                                txt.Line($"{_response.Prescripcion}");
                                txt.Line("Alergias: ")
                                .FontColor(Colors.Blue.Darken4);
                                txt.Line($"{_response.Paciente.Alergias}");

                            });
                        });
                        columna.Item().LineHorizontal(1.8f).LineColor(Colors.Blue.Darken4);

                        columna.Item().Background(Colors.LightBlue.Lighten5).Column(fila =>
                        {
                            fila.Item().Padding(5).PaddingTop(5).Row(row =>
                            {
                                row.RelativeItem().Text(txt =>
                                {
                                    txt.Span("Correo: ")
                                    .FontSize(11)
                                    .FontColor(Colors.Blue.Darken4)
                                    .Underline()
                                    .Bold();

                                    txt.Span($"{_response.Medico.Correo}")
                                    .FontSize(10)
                                    .FontColor(Colors.Blue.Darken4)
                                    .Underline()
                                    .Bold();
                                });
                                row.ConstantItem(200).Text(txt =>
                                {
                                    txt.Span("Firma: ")
                                    .FontSize(11)
                                    .FontColor(Colors.Blue.Darken4)
                                    .Underline()
                                    .Italic()
                                    .Bold();
                                });
                            });


                            fila.Item().Padding(5).PaddingBottom(5).Text(txt =>
                            {
                                txt.Span("Telefono: ")
                               .FontSize(11)
                               .FontColor(Colors.Blue.Darken4)
                               .Underline()
                               .Bold();

                                txt.Span($"{_response.Medico.Telefono}")
                               .FontSize(10)
                               .FontColor(Colors.Blue.Darken4)
                               .Underline()
                               .Bold();
                            });
                        });
                    });

                } 
            });


        }

    }
}
