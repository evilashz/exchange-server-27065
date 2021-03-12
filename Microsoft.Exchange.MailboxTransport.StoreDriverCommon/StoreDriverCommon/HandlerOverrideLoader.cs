using System;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x02000019 RID: 25
	internal class HandlerOverrideLoader
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00004B94 File Offset: 0x00002D94
		public static void ApplyConfiguredOverrides(ExceptionHandler exceptionHandler, NameValueCollection configurationAppSettings)
		{
			foreach (string text in from key in configurationAppSettings.AllKeys
			where key.StartsWith("ExceptionHandler_Override_")
			select key)
			{
				HandlerOverrideLoader.Diag.TraceDebug<string>(0L, "ExceptionHandlerOverrides: Begin processing override for key {0}", text);
				string[] array = text.Split(new char[]
				{
					'_'
				});
				if (array.Length != 4)
				{
					throw new HandlerParseException(string.Format("Unable to parse configured override because there were not the correct number of components in the key. Key: {0}", text));
				}
				string name = array[2].Trim();
				Type type = null;
				string text2 = array[3].Trim();
				foreach (string arg in HandlerOverrideLoader.wellKnownExceptionAssemblies)
				{
					type = Type.GetType(string.Format("{0}, {1}", text2, arg));
					if (type != null)
					{
						break;
					}
				}
				if (type == null)
				{
					type = Type.GetType(text2);
				}
				if (!(type != null))
				{
					throw new HandlerParseException(string.Format("ExceptionHandlerOverrides: Unable to determine the following exception type: {0}", text2));
				}
				exceptionHandler.AddOrUpdateOverrideHandler(name, type, Handler.Parse(configurationAppSettings[text]));
				HandlerOverrideLoader.Diag.TraceDebug<string>(0L, "ExceptionHandlerOverrides: Applied override for key {0}", text);
				HandlerOverrideLoader.Diag.TraceDebug<string>(0L, "ExceptionHandlerOverrides: Completed processing override for key {0}", text);
			}
		}

		// Token: 0x04000054 RID: 84
		private const string AppSettingsOverrideKeyNamePrefix = "ExceptionHandler_Override_";

		// Token: 0x04000055 RID: 85
		private const string ComponentKeyName = "Component";

		// Token: 0x04000056 RID: 86
		private const string ExceptionKeyName = "Exception";

		// Token: 0x04000057 RID: 87
		private static readonly Trace Diag = ExTraceGlobals.StoreDriverDeliveryTracer;

		// Token: 0x04000058 RID: 88
		private static string[] wellKnownExceptionAssemblies = new string[]
		{
			"Microsoft.Exchange.StoreProvider",
			"Microsoft.Exchange.Data.Storage"
		};
	}
}
