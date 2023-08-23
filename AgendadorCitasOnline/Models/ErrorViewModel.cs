namespace AgendadorCitasOnline.Models
{
    public class ErrorViewModel
    {
        // ID de la solicitud que generó el error
        public string? RequestId { get; set; }

        // Propiedad que indica si el ID de la solicitud debe mostrarse
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}