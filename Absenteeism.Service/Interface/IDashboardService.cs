using Absenteeism.Models.Domain;
using Absenteeism.Models.Input;
using Absenteeism.Models.Output;

namespace Absenteeism.Service.Interface
{
    public interface IDashboardService
    {
        Task<GetWorkingHistory> SavetblWorkingHistory(tblWorkingHistory tblWorkingHistory);
        Task<List<GetWorkingHistory>> GettblWorkingHistory(string StartDate, string EndDate, int PlantId, int UserId);
        Task<GetWorkingHistory> UpdatetblWorkingHistory(tblWorkingHistory tblWorkingHistory);
        Task DeleteWorkHistory(int Id);

        Task<GettblDefect> SavetblDefect(tblDefect tblDefect);
        Task DeletetblDefect(int Id);
        Task<GettblDefect> UpdatetblDefect(tblDefect tblDefect);

        Task<List<GettblDefect>> GettblDefect(int PlantId);

        Task<GetAvailability> CreateAvailability(tblAvailability inputtblAvailability);
        Task DeletetblAvailability(int Id);

        Task<GetAvailability> UpdateAvailability(UpdateAvailiability inputtblAvailability);

        Task<List<GetAvailability>> GettblAvailability(string StartDate, string EndDate, int PlantId, int UserId);
        Task<GettblCellMemberStatus> CreateCellMember(tblCellMemberStatus inputtblCellMemberStatus);
        Task DeleteCellMember(int Id);
        Task<GettblCellMemberStatus> UpdatetblCellMemberStatus(tblCellMemberStatus inputtblCellMemberStatus);
        Task<List<GettblCellMemberStatus>> GettblCellMemberStatus(string StartDate, string EndDate, int ShiftId, int PlantId);

        Task<List<GettblAllocation>> CreatetblAllocation(InputAllocation inputtblAllocation);
        Task DeletetblAllocation(int Id);
        Task<GettblAllocation> UpdatetblAllocation(tblAllocation inputtblAllocation);
        Task<List<GettblAllocation>> GettodayAllocationCellMember(int PlantId, int UserId);

        Task<List<GettblAllocation>> GettblAllocation(int PlantId, int UserId, string StartDate, string EndDate, int? ShiftId);

    }
}
