using System;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using Microsoft.Online.Administration.WebService;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Provisioning
{
	// Token: 0x0200029D RID: 669
	public class BecWebServiceProxy
	{
		// Token: 0x06001306 RID: 4870 RVA: 0x00085548 File Offset: 0x00083748
		public BecWebServiceProxy(string becWebServiceEndpoint)
		{
			this.becWebServiceUri = becWebServiceEndpoint;
			WSHttpBinding wshttpBinding = new WSHttpBinding
			{
				SendTimeout = new TimeSpan(1, 0, 0),
				MaxReceivedMessageSize = 50000000L
			};
			wshttpBinding.Security.Mode = SecurityMode.Transport;
			wshttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
			EndpointAddress remoteAddress = new EndpointAddress(this.becWebServiceUri);
			this.client = new ProvisioningWebServiceClient(wshttpBinding, remoteAddress);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x000855BC File Offset: 0x000837BC
		public void SetCurrentUserCredential(string userName, string password)
		{
			if (string.IsNullOrEmpty(userName))
			{
				throw new ArgumentException("userName");
			}
			if (string.IsNullOrEmpty(password))
			{
				throw new ArgumentException("password");
			}
			int startIndex = this.becWebServiceUri.IndexOf('.');
			string siteName = "https://ps" + this.becWebServiceUri.Substring(startIndex);
			string federationProviderId = this.becWebServiceUri.Contains("partner.microsoftonline.cn") ? "partner.microsoftonline.cn" : "microsoftonline.com";
			int num = 0;
			for (;;)
			{
				num++;
				try
				{
					LiveIdentityManager liveIdentityManager = new LiveIdentityManager();
					this.liveToken = liveIdentityManager.LogOnUser(federationProviderId, userName, password, siteName, "MCMBI", null);
					this.currentContext = null;
				}
				catch
				{
					if (num >= 3)
					{
						throw;
					}
					continue;
				}
				break;
			}
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0008567C File Offset: 0x0008387C
		public Response Invoke(string methodName, Request request)
		{
			if (string.IsNullOrEmpty(methodName))
			{
				throw new ArgumentException("methodName");
			}
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (string.IsNullOrEmpty(this.liveToken))
			{
				throw new InvalidOperationException("Please call SetCurrentUserCredential before making the Invoke call.");
			}
			MethodInfo method = typeof(ProvisioningWebServiceClient).GetMethod(methodName);
			if (method == null)
			{
				throw new ArgumentException("Method name is not found.", "methodName");
			}
			Response result;
			using (new OperationContextScope(this.client.InnerChannel))
			{
				OperationContext operationContext = OperationContext.Current;
				operationContext.OutgoingMessageHeaders.Clear();
				if (this.currentContext != null)
				{
					MessageHeader header = MessageHeader.CreateHeader("BecContext", "http://becwebservice.microsoftonline.com/", this.currentContext);
					operationContext.OutgoingMessageHeaders.Add(header);
				}
				MessageHeader header2 = MessageHeader.CreateHeader("UserIdentityHeader", "http://provisioning.microsoftonline.com/", new UserIdentityHeader
				{
					LiveToken = this.liveToken
				});
				operationContext.OutgoingMessageHeaders.Add(header2);
				ClientVersionHeader value = new ClientVersionHeader
				{
					ClientId = new Guid("50AFCE61-C917-435b-8C6D-60AA5A8B8AA7"),
					Version = "1.0.0.0"
				};
				MessageHeader header3 = MessageHeader.CreateHeader("ClientVersionHeader", "http://provisioning.microsoftonline.com/", value);
				operationContext.OutgoingMessageHeaders.Add(header3);
				int num = 3;
				int num2 = 0;
				for (;;)
				{
					try
					{
						result = (Response)method.Invoke(this.client, new object[]
						{
							request
						});
						break;
					}
					catch (Exception ex)
					{
						if (num2 >= num)
						{
							throw new Exception(ex.ToString(), ex);
						}
						num2++;
						if (ex.ToString().Contains("ThrottlingException"))
						{
							Thread.Sleep(30000);
						}
						else
						{
							Thread.Sleep(3000);
						}
					}
					finally
					{
						if (operationContext.IncomingMessageHeaders != null && operationContext.IncomingMessageHeaders.FindHeader("BecContext", "http://becwebservice.microsoftonline.com/") >= 0)
						{
							this.currentContext = operationContext.IncomingMessageHeaders.GetHeader<Context>("BecContext", "http://becwebservice.microsoftonline.com/");
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000E65 RID: 3685
		private const int ThrottlingExceptionSleep = 30000;

		// Token: 0x04000E66 RID: 3686
		private const int OtherExceptionSleep = 3000;

		// Token: 0x04000E67 RID: 3687
		private const string ContextHeaderNamespace = "http://becwebservice.microsoftonline.com/";

		// Token: 0x04000E68 RID: 3688
		private const string PublicHeadersNamespace = "http://provisioning.microsoftonline.com/";

		// Token: 0x04000E69 RID: 3689
		private const string ContextHeaderName = "BecContext";

		// Token: 0x04000E6A RID: 3690
		private const string UserIdentityHeaderName = "UserIdentityHeader";

		// Token: 0x04000E6B RID: 3691
		private const string ClientVersionHeaderName = "ClientVersionHeader";

		// Token: 0x04000E6C RID: 3692
		private const string ClientVersionHeaderId = "50AFCE61-C917-435b-8C6D-60AA5A8B8AA7";

		// Token: 0x04000E6D RID: 3693
		private readonly string becWebServiceUri;

		// Token: 0x04000E6E RID: 3694
		private ProvisioningWebServiceClient client;

		// Token: 0x04000E6F RID: 3695
		private string liveToken;

		// Token: 0x04000E70 RID: 3696
		private Context currentContext;
	}
}
