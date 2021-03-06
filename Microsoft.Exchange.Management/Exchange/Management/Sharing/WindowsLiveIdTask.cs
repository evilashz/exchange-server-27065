using System;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Text;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.LiveServicesHelper;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Sharing
{
	// Token: 0x02000D87 RID: 3463
	public abstract class WindowsLiveIdTask : Task
	{
		// Token: 0x06008503 RID: 34051 RVA: 0x0022015C File Offset: 0x0021E35C
		internal bool TryExecuteWebMethod(LiveIdInstanceType liveIdInstanceType, out XmlDocument xmlDocument)
		{
			bool result = false;
			xmlDocument = null;
			try
			{
				xmlDocument = this.WindowsLiveIdMethod(liveIdInstanceType);
				result = true;
			}
			catch (UriFormatException exception)
			{
				this.LogError(exception, ErrorCategory.InvalidArgument);
			}
			catch (XmlException exception2)
			{
				this.LogError(exception2, ErrorCategory.InvalidResult);
			}
			catch (SoapException ex)
			{
				if (!ex.Detail.InnerText.Contains("0x80045a64") && !ex.Detail.InnerText.Contains("0x80048163"))
				{
					this.LogError(ex, ErrorCategory.InvalidOperation);
				}
			}
			catch (WebException exception3)
			{
				this.LogError(exception3, ErrorCategory.InvalidOperation);
			}
			catch (LiveServicesHelperConfigException exception4)
			{
				this.LogError(exception4, ErrorCategory.MetadataError);
			}
			catch (LiveServicesException exception5)
			{
				this.LogError(exception5, ErrorCategory.MetadataError);
			}
			catch (XPathException exception6)
			{
				this.LogError(exception6, ErrorCategory.InvalidResult);
			}
			return result;
		}

		// Token: 0x06008504 RID: 34052 RVA: 0x0022025C File Offset: 0x0021E45C
		internal void LogError(Exception exception, ErrorCategory category)
		{
			base.WriteVerbose(Strings.ExceptionOccured(this.GetExtendedExceptionInformation(exception)));
		}

		// Token: 0x06008505 RID: 34053
		protected abstract XmlDocument WindowsLiveIdMethod(LiveIdInstanceType liveIdInstanceType);

		// Token: 0x06008506 RID: 34054
		protected abstract ConfigurableObject ParseResponse(LiveIdInstanceType liveIdInstanceType, XmlDocument xmlResponse);

		// Token: 0x06008507 RID: 34055 RVA: 0x00220270 File Offset: 0x0021E470
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			foreach (object obj in Enum.GetValues(typeof(LiveIdInstanceType)))
			{
				LiveIdInstanceType liveIdInstanceType = (LiveIdInstanceType)obj;
				XmlDocument xmlDocument = null;
				if (this.TryExecuteWebMethod(liveIdInstanceType, out xmlDocument) && xmlDocument != null)
				{
					base.WriteObject(this.ParseResponse(liveIdInstanceType, xmlDocument));
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008508 RID: 34056 RVA: 0x002202F4 File Offset: 0x0021E4F4
		private string GetExtendedExceptionInformation(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			if (!(exception is WebException))
			{
				if (!(exception is SoapException))
				{
					goto IL_14A;
				}
			}
			while (exception != null)
			{
				WebException ex = exception as WebException;
				if (ex != null)
				{
					stringBuilder.Append("WebException.Response = ");
					if (ex.Response != null)
					{
						using (Stream responseStream = ex.Response.GetResponseStream())
						{
							if (responseStream.CanRead)
							{
								if (responseStream.CanSeek)
								{
									responseStream.Seek(0L, SeekOrigin.Begin);
								}
								using (StreamReader streamReader = new StreamReader(responseStream))
								{
									stringBuilder.AppendLine();
									stringBuilder.AppendLine(streamReader.ReadToEnd());
									goto IL_A0;
								}
							}
							stringBuilder.AppendLine("<cannot read response stream>");
							IL_A0:
							goto IL_B8;
						}
					}
					stringBuilder.AppendLine("<Null>");
				}
				IL_B8:
				SoapException ex2 = exception as SoapException;
				if (ex2 != null)
				{
					if (ex2.Code != null)
					{
						stringBuilder.Append("SoapException.Code = ");
						stringBuilder.Append(ex2.Code);
						stringBuilder.AppendLine();
					}
					if (ex2.Detail != null)
					{
						stringBuilder.AppendLine("SoapException.Detail = ");
						stringBuilder.AppendLine(ex2.Detail.OuterXml);
					}
				}
				stringBuilder.AppendLine("Exception:");
				stringBuilder.AppendLine(exception.ToString());
				stringBuilder.AppendLine();
				exception = exception.InnerException;
			}
			IL_14A:
			return stringBuilder.ToString();
		}
	}
}
