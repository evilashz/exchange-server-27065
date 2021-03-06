using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.ObjectModel;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x0200001A RID: 26
	internal sealed class ConfigObjectReader
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00004F2C File Offset: 0x0000312C
		private static void FillObjects(DataSourceManager[] managers, ConfigObject[] configObjects, Type objectType)
		{
			if (configObjects == null)
			{
				return;
			}
			foreach (ConfigObject configObject in configObjects)
			{
				for (int j = 1; j < managers.Length; j++)
				{
					string text = (string)configObject.Fields[DataSourceManager.LinkIdName];
					ExTraceGlobals.ConfigObjectTracer.Information<DataSourceManager, string>(0L, "ConfigObjectReader::FillObjects - reading from {0}, linkId={1}.", managers[j], (text == null) ? "NULL" : text);
					managers[j].ReadLinked(configObject, objectType, text);
				}
				ExTraceGlobals.ConfigObjectTracer.Information<string, object>(0L, "ConfigObjectReader::FillObjects - Initializing {0} {1}", configObject.GetType().Name, configObject.Fields["Identity"]);
				configObject.InitializeDefaults();
				configObject.Fields.ResetChangeTracking();
				configObject.Validate();
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004FF0 File Offset: 0x000031F0
		public static ConfigObjectCollection FindAll(Type objectType)
		{
			if (null == objectType)
			{
				throw new ArgumentNullException("objectType");
			}
			DataSourceManager[] dataSourceManagers = DataSourceManager.GetDataSourceManagers(objectType, "Identity");
			ConfigObject[] array = dataSourceManagers[0].Find(objectType, string.Empty, true, null);
			ConfigObjectReader.FillObjects(dataSourceManagers, array, objectType);
			return new ConfigObjectCollection(array);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000503C File Offset: 0x0000323C
		public static ConfigObject FindById(Type objectType, string objectIdentity)
		{
			if (null == objectType)
			{
				throw new ArgumentNullException("objectType");
			}
			if (string.IsNullOrEmpty(objectIdentity))
			{
				throw new ArgumentException("Argument 'objectIdentity' was null or emtpy.");
			}
			DataSourceManager[] dataSourceManagers = DataSourceManager.GetDataSourceManagers(objectType, "Identity");
			ConfigObject configObject = dataSourceManagers[0].Read(objectType, objectIdentity);
			if (configObject != null)
			{
				ConfigObject[] configObjects = new ConfigObject[]
				{
					configObject
				};
				ConfigObjectReader.FillObjects(dataSourceManagers, configObjects, objectType);
			}
			return configObject;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000050A0 File Offset: 0x000032A0
		public static object StartRange(string identity, string property, Type classType, int pageSize)
		{
			if (null == classType)
			{
				throw new ArgumentNullException("classType");
			}
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentException("Argument 'identity' was null or emtpy.");
			}
			if (string.IsNullOrEmpty(property))
			{
				throw new ArgumentException("Argument 'property' was null or emtpy.");
			}
			DataSourceManager[] dataSourceManagers = DataSourceManager.GetDataSourceManagers(classType, property);
			ConfigObjectReader.RangeContext rangeContext = new ConfigObjectReader.RangeContext();
			rangeContext.Dsm = dataSourceManagers[0];
			rangeContext.Context = rangeContext.Dsm.StartRange(identity, property, pageSize);
			return rangeContext;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005114 File Offset: 0x00003314
		public static bool NextRange(object context, List<object> resultStore)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (resultStore == null)
			{
				throw new ArgumentNullException("resultStore");
			}
			ConfigObjectReader.RangeContext rangeContext = (ConfigObjectReader.RangeContext)context;
			if (rangeContext.Dsm == null)
			{
				throw new ArgumentNullException("rangeContext.Dsm");
			}
			return rangeContext.Dsm.NextRange(rangeContext.Context, resultStore);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000516C File Offset: 0x0000336C
		public static void EndRange(object context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ConfigObjectReader.RangeContext rangeContext = (ConfigObjectReader.RangeContext)context;
			if (rangeContext.Dsm == null)
			{
				throw new ArgumentNullException("rangeContext.Dsm");
			}
			rangeContext.Dsm.EndRange(rangeContext.Context);
		}

		// Token: 0x0200001B RID: 27
		private class RangeContext
		{
			// Token: 0x04000054 RID: 84
			internal DataSourceManager Dsm;

			// Token: 0x04000055 RID: 85
			internal object Context;
		}
	}
}
