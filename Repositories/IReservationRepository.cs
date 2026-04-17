using reservations_api.Models.Entities;

namespace reservations_api.Repositories;

public interface IReservationRepository
{
  Task<Reservation> AddAsync(Reservation reservation);
  Task<List<Reservation>> GetByClassroomAndDateAsync(Guid classroomId, DateOnly date);

  Task<bool> DeleteReservation(Guid id);


  Task<Reservation?> GetByIdAsync(int id);
  
  Task SaveChangesAsync();
}