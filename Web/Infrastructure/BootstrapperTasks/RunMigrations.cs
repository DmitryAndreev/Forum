namespace Web.Infrastructure.BootstrapperTasks
{
    using System;
    using Castle.MicroKernel;
    using JetBrains.Annotations;
    using log4net;
    using Migrations;
    using MvcExtensions;

    [UsedImplicitly]
    public class RunMigrations : BootstrapperTask
    {
        private readonly ILog _logger;

        public RunMigrations(IKernel kernel)
        {
            _logger = kernel.Resolve<ILog>();
        }

        public override TaskContinuation Execute()
        {
            try
            {
                var migrationLog = MigrationsRunner.Run();
                
                _logger.DebugFormat("\n\n{0}", migrationLog);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Ошибка при применении миграций БД", ex);
#if DEBUG
                throw;
#endif
            }

            return TaskContinuation.Continue;
        }
    }
}