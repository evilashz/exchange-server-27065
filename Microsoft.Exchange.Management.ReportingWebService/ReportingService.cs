using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Services;
using System.Data.Services.Providers;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Library.Annotations;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000036 RID: 54
	[DiagnosticsBehavior]
	[ReportingBehavior]
	internal class ReportingService : DataService<IReportingDataSource>, IServiceProvider
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00006BDC File Offset: 0x00004DDC
		public static void InitializeService(DataServiceConfiguration config)
		{
			config.SetEntitySetAccessRule("*", 3);
			config.UseVerboseErrors = Global.GetAppSettingAsBool("IncludeVerboseErrorsInReponse", false);
			config.DataServiceBehavior.MaxProtocolVersion = 2;
			int num;
			if (!int.TryParse(ConfigurationManager.AppSettings["EntitysetPageSize"], out num))
			{
				num = 1000;
			}
			config.SetEntitySetPageSize("*", num);
			config.AnnotationsBuilder = new Func<IEdmModel, IEnumerable<IEdmModel>>(ReportingService.BuildAnnotations);
			bool flag;
			bool.TryParse(ConfigurationManager.AppSettings[ReportingService.AppSettingEnableRwsVersionZeroKey], out flag);
			if (flag)
			{
				ReportingVersion.EnableVersionZero();
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006CA8 File Offset: 0x00004EA8
		public object GetService(Type serviceType)
		{
			object provider = null;
			if (serviceType == typeof(IDataServiceMetadataProvider))
			{
				ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.GetMetadataProviderLatency, delegate
				{
					provider = this.GetMetadataProvider();
				});
			}
			if (serviceType == typeof(IDataServiceQueryProvider))
			{
				ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.GetQueryProviderLatency, delegate
				{
					provider = this.GetQueryProvider(this.metadataProvider);
				});
			}
			return provider;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006D25 File Offset: 0x00004F25
		protected override void HandleException(HandleExceptionArgs args)
		{
			base.HandleException(args);
			ServiceDiagnostics.ReportUnhandledException(args.Exception, HttpContext.Current);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006D50 File Offset: 0x00004F50
		private static IEnumerable<IEdmModel> BuildAnnotations(IEdmModel model)
		{
			IEdmEntityContainer edmEntityContainer = ExtensionMethods.EntityContainers(model).SingleOrDefault((IEdmEntityContainer ec) => ec.Name == "TenantReportingWebService");
			EdmModel edmModel = new EdmModel();
			edmModel.AddReferencedModel(model);
			ReportingSchema reportingSchema = ReportingService.GetReportingSchema();
			foreach (IEntity entity in reportingSchema.Entities.Values)
			{
				IEdmEntitySet edmEntitySet = edmEntityContainer.FindEntitySet(entity.Name);
				if (edmEntitySet != null)
				{
					ReportingService.AddAnnotation(edmModel, edmEntitySet, "ReportTitle", entity.Annotation.ReportTitle);
					if (entity.Annotation.Xaxis != null)
					{
						string value = string.Join(",", entity.Annotation.Xaxis);
						if (!string.IsNullOrEmpty(value))
						{
							ReportingService.AddAnnotation(edmModel, edmEntitySet, "X-Axis", value);
						}
					}
					ReportingService.AddAnnotation(edmModel, edmEntitySet, "Y-Axis", string.Join(",", entity.Annotation.Yaxis));
				}
			}
			return new IEdmModel[]
			{
				edmModel
			};
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006E7C File Offset: 0x0000507C
		private static void AddAnnotation(EdmModel annotationModel, IEdmEntitySet entitySet, string name, string value)
		{
			EdmValueTerm edmValueTerm = new EdmValueTerm("TenantReporting", name, 14);
			EdmValueAnnotation edmValueAnnotation = new EdmValueAnnotation(entitySet, edmValueTerm, new EdmStringConstant(value));
			annotationModel.AddVocabularyAnnotation(edmValueAnnotation);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006EAC File Offset: 0x000050AC
		private static ReportingSchema GetReportingSchema()
		{
			HttpContext httpContext = HttpContext.Current;
			string currentReportingVersion = ReportingVersion.GetCurrentReportingVersion(httpContext);
			return ReportingSchema.GetReportingSchema(currentReportingVersion);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006ED0 File Offset: 0x000050D0
		private IDataServiceMetadataProvider GetMetadataProvider()
		{
			this.metadataProvider = (ReportingMetadataProvider)HttpRuntime.Cache[this.GetCacheKey()];
			if (this.metadataProvider != null)
			{
				return this.metadataProvider;
			}
			ReportingSchema reportingSchema = ReportingService.GetReportingSchema();
			this.metadataProvider = new ReportingMetadataProvider(reportingSchema);
			HttpRuntime.Cache.Insert(this.GetCacheKey(), this.metadataProvider, null, (DateTime)ExDateTime.UtcNow.Add(ReportingService.MetadataProviderCacheMaxAge), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
			return this.metadataProvider;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006F54 File Offset: 0x00005154
		private IDataServiceQueryProvider GetQueryProvider(IDataServiceMetadataProvider metadata)
		{
			if (this.queryProvider == null)
			{
				ReportingSchema reportingSchema = ReportingService.GetReportingSchema();
				this.queryProvider = new ReportingQueryProvider(metadata, reportingSchema);
			}
			return this.queryProvider;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006F84 File Offset: 0x00005184
		private string GetCacheKey()
		{
			HttpContext httpContext = HttpContext.Current;
			string currentReportingVersion = ReportingVersion.GetCurrentReportingVersion(httpContext);
			return string.Format(CultureInfo.InvariantCulture, "Exchange_Reporting_Metadata_{0}_{1}", new object[]
			{
				RbacPrincipal.Current.CacheKeys[0],
				currentReportingVersion
			});
		}

		// Token: 0x0400009F RID: 159
		private const string AppSettingIncludeVerboseErrorsInReponse = "IncludeVerboseErrorsInReponse";

		// Token: 0x040000A0 RID: 160
		private const int DefaultPageSize = 1000;

		// Token: 0x040000A1 RID: 161
		private const string AppSettingEntitysetPageSize = "EntitysetPageSize";

		// Token: 0x040000A2 RID: 162
		public static readonly string AppSettingEnableRwsVersionZeroKey = "EnableRwsVersionZero";

		// Token: 0x040000A3 RID: 163
		private static readonly TimeSpan MetadataProviderCacheMaxAge = new TimeSpan(0, 15, 0);

		// Token: 0x040000A4 RID: 164
		private IDataServiceMetadataProvider metadataProvider;

		// Token: 0x040000A5 RID: 165
		private IDataServiceQueryProvider queryProvider;
	}
}
