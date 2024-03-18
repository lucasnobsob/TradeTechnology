using Domain.Entities;

namespace Domain.Ports
{
    public interface IJWTService
    {
        string GerarToken(User user);
    }
}
