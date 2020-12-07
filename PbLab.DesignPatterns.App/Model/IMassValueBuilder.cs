namespace PbLab.DesignPatterns.Model
{
    public interface IMassValueBuilder
    {
        void AddValue(string value);

        void AddUnit(string unit);

        MassValue Build();
    }
}