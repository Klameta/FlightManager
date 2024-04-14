using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Flight : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "From field is required.")]
        public string From { get; set; }

        [Required(ErrorMessage = "To field is required.")]
        public string To { get; set; }

        [Required(ErrorMessage = "Departure date is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Departure Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "Arrival date is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Arrival Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ArrivalDate { get; set; }

        [Required(ErrorMessage = "Type field is required.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Plane number is required.")]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Plane number must contain only uppercase letters and digits.")]
        public string PlaneNumber { get; set; }

        [Required(ErrorMessage = "Pilot name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Pilot name must be between 2 and 100 characters.")]
        public string PilotName { get; set; }

        [Required(ErrorMessage = "Capacity field is required.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Capacity must be a numeric value.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Capacity for VIP passengers is required.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Capacity for VIP passengers must be a numeric value.")]
        public int CapacityVIP { get; set; }

        [NotMapped]
        public string FlightDuration => $"{(ArrivalDate - DepartureDate).TotalHours:F2} hours";

        public ICollection<Reservation> Reservations { get; set; }

        public Flight()
        {
            Reservations = new List<Reservation>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ArrivalDate < DepartureDate)
            {
                yield return new ValidationResult("Arrival date cannot be before departure date.", new[] { nameof(ArrivalDate) });
            }
        }
    }
}
