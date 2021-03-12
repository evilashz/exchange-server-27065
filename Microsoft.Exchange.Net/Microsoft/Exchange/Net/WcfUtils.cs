using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000B11 RID: 2833
	internal static class WcfUtils
	{
		// Token: 0x06003D08 RID: 15624 RVA: 0x0009EDA0 File Offset: 0x0009CFA0
		public static void DisposeWcfClientGracefully(ICommunicationObject client, bool skipDispose = false)
		{
			if (client == null)
			{
				return;
			}
			bool flag = false;
			try
			{
				if (client.State != CommunicationState.Faulted)
				{
					client.Close();
					flag = true;
				}
			}
			catch (TimeoutException)
			{
			}
			catch (CommunicationException)
			{
			}
			catch (InvalidOperationException)
			{
			}
			finally
			{
				if (!flag)
				{
					client.Abort();
				}
				if (!skipDispose)
				{
					((IDisposable)client).Dispose();
				}
			}
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x0009EE20 File Offset: 0x0009D020
		public static ChannelFactory<TClient> TryCreateChannelFactoryFromConfig<TClient>(string endpointName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("endpointName", endpointName);
			ClientSection clientSection = (ClientSection)ConfigurationManager.GetSection("system.serviceModel/client");
			if (clientSection != null)
			{
				bool flag = false;
				foreach (object obj in clientSection.Endpoints)
				{
					ChannelEndpointElement channelEndpointElement = (ChannelEndpointElement)obj;
					if (endpointName.Equals(channelEndpointElement.Name, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					return new ChannelFactory<TClient>(endpointName);
				}
			}
			return null;
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x0009EEB4 File Offset: 0x0009D0B4
		public static LocalizedString FullExceptionMessage(Exception ex)
		{
			return WcfUtils.FullExceptionMessage(ex, false);
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x0009EEC0 File Offset: 0x0009D0C0
		public static LocalizedString FullExceptionMessage(Exception ex, bool includeStack)
		{
			LocalizedString localizedString = LocalizedString.Empty;
			string stackTrace = ex.StackTrace;
			int num = 0;
			while (ex != null && num < 20)
			{
				LocalizedException ex2 = ex as LocalizedException;
				LocalizedString localizedString2 = (ex2 != null) ? ex2.LocalizedString : new LocalizedString(ex.Message);
				localizedString = ((num > 0) ? NetServerException.NestedExceptionMsg(localizedString, localizedString2) : localizedString2);
				num++;
				ex = ex.InnerException;
			}
			if (includeStack)
			{
				localizedString = NetServerException.ExceptionWithStack(localizedString, stackTrace);
			}
			return localizedString;
		}
	}
}
