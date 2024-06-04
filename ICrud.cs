using System.Collections.ObjectModel;

namespace MaquetteBotanic;

public interface ICrud
{
    public int Create();
    public int Delete();
    public int Update();
}