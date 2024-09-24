using Absenteeism.Models.Domain;
using Absenteeism.Models.Input;
using Absenteeism.Models.Output;

namespace Absenteeism.Repository.Interface
{
    public interface IDashboardRepository
    {
        Task<GetWorkingHistory> CreatetblWorkingHistory(tblWorkingHistory tblWorkingHistory);
        Task<GetWorkingHistory> UpdatetblWorkingHistory(tblWorkingHistory tblWorkingHistory);
        Task DeleteWorkHistory(int Id);

        Task<List<GetWorkingHistory>> GettblWorkingHistory(string StartDate, string EndDate, int PlantId, int UserId);

        Task<GettblDefect> SavetblDefect(tblDefect tblDefect);
        Task DeletetblDefect(int Id);

        Task<GettblDefect> UpdatetblDefect(tblDefect tblDefect);

        Task<List<GettblDefect>> GettblDefect(int PlantId);

        Task<GetAvailability> CreateAvailability(tblAvailability tblAvailability);
        Task DeletetblAvailability(int Id);

        Task<GetAvailability> UpdateAvailability(UpdateAvailiability inputtblAvailability);

        Task<List<GetAvailability>> GetAvailability(string StartDate, string EndDate, int PlantId, int UserId);

        Task<GettblCellMemberStatus> CreatetblCellMemberStatus(tblCellMemberStatus inputtblCellMemberStatus);

        Task DeleteCellMemberStatus(int Id);

        Task<GettblCellMemberStatus> UpdatetblCellMemberStatus(tblCellMemberStatus inputtblCellMemberStatus);

        Task<List<GettblCellMemberStatus>> GettblCellMemberStatus(string StartDate, string EndDate, int ShiftId, int PlantId);
        Task<List<GettblAllocation>> CreatetblAllocation(InputAllocation inputtblAllocation);
        Task DeleteAllocation(int Id);
        Task<GettblAllocation> UpdatetblAllocation(tblAllocation inputtblAllocation);
        Task<List<GettblAllocation>> GettblAllocation(int PlantId,int UserId,string StartDate,string EndDate,int? ShiftId);
        Task<List<GettblAllocation>> GettodayAllocationCellMember(int PlantId,int UserId);
    }
}
