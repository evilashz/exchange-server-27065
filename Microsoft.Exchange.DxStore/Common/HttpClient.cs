using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.DxStore;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200006E RID: 110
	public class HttpClient
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		public static async Task SendMessageAsync(string targetServer, string nodeName, string groupName, HttpRequest.PaxosMessage msg)
		{
			try
			{
				string uri = HttpConfiguration.FormClientUriPrefix(targetServer, nodeName, groupName);
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
				req.Method = "PUT";
				req.ContentType = "application/octet-stream";
				MemoryStream ms = DxSerializationUtil.SerializeMessage(msg);
				req.ContentLength = ms.Length;
				ms.Position = 0L;
				Stream outStream = await req.GetRequestStreamAsync();
				using (outStream)
				{
					await ms.CopyToAsync(outStream);
				}
				ExTraceGlobals.PaxosMessageTracer.TraceDebug<long>(0L, "Sent PaxosMessage len={0}", ms.Length);
				using ((HttpWebResponse)(await req.GetResponseAsync()))
				{
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.PaxosMessageTracer.TraceError<string, Exception>(0L, "SendMessageAsync failed to node {0} caught: {1}", nodeName, ex);
				throw new DxStoreInstanceClientException(ex.Message, ex);
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00010218 File Offset: 0x0000E418
		public static async Task<InstanceStatusInfo> GetStatusAsync(string targetServer, string targetNodeName, string groupName, string sendingNodeName)
		{
			InstanceStatusInfo reply;
			try
			{
				string uri = HttpConfiguration.FormClientUriPrefix(targetServer, targetNodeName, groupName);
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
				req.Method = "PUT";
				req.ContentType = "application/octet-stream";
				HttpRequest.GetStatusRequest reqMsg = new HttpRequest.GetStatusRequest(sendingNodeName);
				MemoryStream ms = DxSerializationUtil.SerializeMessage(reqMsg);
				req.ContentLength = ms.Length;
				ms.Position = 0L;
				Stream outStream = await req.GetRequestStreamAsync();
				using (outStream)
				{
					await ms.CopyToAsync(outStream);
				}
				using (HttpWebResponse httpResponse = (HttpWebResponse)(await req.GetResponseAsync()))
				{
					using (Stream responseStream = httpResponse.GetResponseStream())
					{
						HttpReply httpReply = DxSerializationUtil.Deserialize<HttpReply>(responseStream);
						if (httpReply is HttpReply.GetInstanceStatusReply)
						{
							reply = (httpReply as HttpReply.GetInstanceStatusReply).Reply;
						}
						else
						{
							if (httpReply is HttpReply.ExceptionReply)
							{
								Exception exception = (httpReply as HttpReply.ExceptionReply).Exception;
								throw new DxStoreInstanceServerException(exception.Message, exception);
							}
							throw new DxStoreInstanceClientException(string.Format("unexpected reply: {0}", httpReply.GetType().FullName));
						}
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.InstanceClientTracer.TraceError<string, Exception>(0L, "GetStatusAsync to {0} caught: {1}", targetNodeName, ex);
				if (ex is DxStoreInstanceClientException || ex is DxStoreInstanceServerException)
				{
					throw;
				}
				throw new DxStoreInstanceClientException(ex.Message, ex);
			}
			return reply;
		}

		// Token: 0x0200006F RID: 111
		public class TargetInfo
		{
			// Token: 0x060004AE RID: 1198 RVA: 0x0001027E File Offset: 0x0000E47E
			public TargetInfo(string host, string node, string groupName)
			{
				this.TargetHost = host;
				this.TargetNode = node;
				this.GroupName = groupName;
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x060004AF RID: 1199 RVA: 0x0001029B File Offset: 0x0000E49B
			// (set) Token: 0x060004B0 RID: 1200 RVA: 0x000102A3 File Offset: 0x0000E4A3
			public string TargetHost { get; set; }

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x060004B1 RID: 1201 RVA: 0x000102AC File Offset: 0x0000E4AC
			// (set) Token: 0x060004B2 RID: 1202 RVA: 0x000102B4 File Offset: 0x0000E4B4
			public string TargetNode { get; set; }

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x060004B3 RID: 1203 RVA: 0x000102BD File Offset: 0x0000E4BD
			// (set) Token: 0x060004B4 RID: 1204 RVA: 0x000102C5 File Offset: 0x0000E4C5
			public string GroupName { get; set; }

			// Token: 0x060004B5 RID: 1205 RVA: 0x000102CE File Offset: 0x0000E4CE
			public static HttpClient.TargetInfo BuildFromNode(string nodeName, InstanceGroupConfig groupConfig)
			{
				if (groupConfig == null)
				{
					return new HttpClient.TargetInfo(nodeName, nodeName, "NoGroupName");
				}
				return new HttpClient.TargetInfo(groupConfig.GetMemberNetworkAddress(nodeName), nodeName, groupConfig.Name);
			}
		}
	}
}
