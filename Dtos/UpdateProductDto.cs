namespace TS.Bootcamp.ECommerce.WebAPI.Dtos
{
    public sealed record UpdateProductDto(Guid Id, string Name, decimal Price)
    {
    }
}
