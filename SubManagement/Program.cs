var builder = WebApplication.CreateBuilder(args);

// ���������� �������� � ���������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ����������� ��������
builder.Services.AddScoped<SubscriptionService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddHostedService<NotificationBackgroundService>();

// ����������� ������� ������
builder.Services.AddHostedService<NotificationBackgroundService>();

var app = builder.Build();

// ��������� Swagger � ������ ����������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();