using System;
using System.Globalization;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000052 RID: 82
	public static class ExportHandlerFactory
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x00017799 File Offset: 0x00015999
		public static IExportHandler CreateExportHandler(IExportContext exportContext, ITracer tracer)
		{
			return ExportHandlerFactory.CreateExportHandler(exportContext, tracer, new ExchangeServiceClientFactory(new UserServiceCallingContextFactory(null)));
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000177B0 File Offset: 0x000159B0
		internal static IExportHandler CreateExportHandler(IExportContext exportContext, ITracer tracer, IServiceClientFactory serviceClientFactory)
		{
			if (exportContext == null)
			{
				throw new ArgumentNullException("exportContext");
			}
			if (exportContext.Sources == null || exportContext.Sources.Count == 0)
			{
				throw new ArgumentNullException("exportContext.Sources");
			}
			for (int i = 0; i < exportContext.Sources.Count; i++)
			{
				ISource source = exportContext.Sources[i];
				if (source == null)
				{
					throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, "exportContext.Source[{0}]", new object[]
					{
						i
					}));
				}
				if (string.IsNullOrEmpty(source.Name))
				{
					throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, "exportContext.Source[{0}].Name", new object[]
					{
						i
					}));
				}
				if (string.IsNullOrEmpty(source.Id))
				{
					throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, "exportContext.Source[{0}].Id", new object[]
					{
						i
					}));
				}
				if (string.IsNullOrEmpty(source.SourceFilter))
				{
					throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, "exportContext.Source[{0}].SourceFilter", new object[]
					{
						i
					}));
				}
			}
			if (exportContext.ExportMetadata == null)
			{
				throw new ArgumentNullException("exportContext.ExportMetadata");
			}
			if (string.IsNullOrEmpty(exportContext.ExportMetadata.ExportName))
			{
				throw new ArgumentNullException("exportContext.ExportMetadata.ExportName");
			}
			if (exportContext.ExportMetadata.ExportStartTime == default(DateTime))
			{
				throw new ArgumentNullException("exportContext.ExportMetadata.ExportStartTime");
			}
			if (exportContext.TargetLocation == null)
			{
				throw new ArgumentNullException("exportContext.TargetLocation");
			}
			PstTarget pstTarget = new PstTarget(exportContext);
			pstTarget.CheckLocation();
			return ExportHandlerFactory.CreateExportHandler(tracer, pstTarget, serviceClientFactory);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001795E File Offset: 0x00015B5E
		internal static IExportHandler CreateExportHandler(ITracer tracer, ITarget target, IServiceClientFactory serviceClientFactory)
		{
			if (tracer != null)
			{
				Tracer.TracerInstance = tracer;
			}
			return new ProgressController(target, serviceClientFactory);
		}
	}
}
