using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.ApplicationLogic.CommonHandlers;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000094 RID: 148
	public abstract class ExchangeDiagnosableWrapper<T> : IDiagnosableExtraData, IDiagnosable
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00017C40 File Offset: 0x00015E40
		protected virtual string UsageText
		{
			get
			{
				return "Returns diagnostics information about specified process.";
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00017C47 File Offset: 0x00015E47
		protected virtual string UsageSample
		{
			get
			{
				return "Get-ExchangeDiagnosticInfo -Server <TargetServer> -Process <ProcessName> -Component <ComponentName> -Argument <Argument>";
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00017C4E File Offset: 0x00015E4E
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return this.ComponentName;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00017C58 File Offset: 0x00015E58
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters arguments)
		{
			XElement xelement = new XElement(this.ComponentName);
			if (arguments.Argument.Equals("?", StringComparison.InvariantCultureIgnoreCase))
			{
				xelement.Add(this.GetUsage());
				return xelement;
			}
			try
			{
				ExTraceGlobals.CommonTracer.TraceDebug<string, bool, string>((long)this.GetHashCode(), "ExchangeDiagnosableWrapper::GetDIagnosticsInfo called. Argument:{0}, AllowRestrictedData:{1}, User:{2}", arguments.Argument, arguments.AllowRestrictedData, arguments.UserIdentity);
				T exchangeDiagnosticsInfoData = this.GetExchangeDiagnosticsInfoData(arguments);
				if (typeof(XElement) == typeof(T))
				{
					xelement.Add(exchangeDiagnosticsInfoData);
				}
				else
				{
					Type type = (exchangeDiagnosticsInfoData != null) ? exchangeDiagnosticsInfoData.GetType() : typeof(T);
					XmlSerializer xmlSerializer = new XmlSerializer(type);
					using (MemoryStream memoryStream = new MemoryStream())
					{
						xmlSerializer.Serialize(memoryStream, exchangeDiagnosticsInfoData);
						memoryStream.Position = 0L;
						xelement.Add(XElement.Load(memoryStream));
					}
				}
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				Exception ex2 = ex;
				do
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append("\r\n");
					}
					stringBuilder.Append(ex2.Message);
					ex2 = ex2.InnerException;
				}
				while (ex2 != null);
				FaultDiagnosticsInfo faultDiagnosticsInfo = new FaultDiagnosticsInfo(-999, string.Format("Type:{0},Ex:{1},Stack:{2}", ex.GetType(), stringBuilder, ex.StackTrace));
				xelement.Add(faultDiagnosticsInfo);
				xelement.Add(faultDiagnosticsInfo.ErrorText);
				ExTraceGlobals.CommonTracer.TraceError<string>((long)this.GetHashCode(), "ExchangeDiagnosableWrapper::GetDiagnosticsInfo Exception occurred. Exception:{0}", ex.ToString());
			}
			return xelement;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00017E14 File Offset: 0x00016014
		void IDiagnosableExtraData.SetData(XElement dataElement)
		{
			this.InternalSetData(dataElement);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00017E1D File Offset: 0x0001601D
		void IDiagnosableExtraData.OnStop()
		{
			this.InternalOnStop();
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000681 RID: 1665
		protected abstract string ComponentName { get; }

		// Token: 0x06000682 RID: 1666 RVA: 0x00017E25 File Offset: 0x00016025
		protected virtual void InternalSetData(XElement dataElement)
		{
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00017E27 File Offset: 0x00016027
		protected virtual void InternalOnStop()
		{
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00017E29 File Offset: 0x00016029
		protected virtual void OnPreProcess()
		{
			ExTraceGlobals.CommonTracer.TraceInformation<string>(0, (long)this.GetHashCode(), "ExchangeDiagnosableWrapper::OnPreProcess called. Component:{0}", (this.ComponentName == null) ? "<null>" : this.ComponentName);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00017E58 File Offset: 0x00016058
		protected virtual string GetUsage()
		{
			StringBuilder stringBuilder = new StringBuilder(this.UsageText);
			stringBuilder.AppendLine(this.UsageSample);
			return stringBuilder.ToString();
		}

		// Token: 0x06000686 RID: 1670
		internal abstract T GetExchangeDiagnosticsInfoData(DiagnosableParameters arguments);

		// Token: 0x06000687 RID: 1671 RVA: 0x00017E84 File Offset: 0x00016084
		internal string RemoveQuotes(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return input;
			}
			return input.Replace("'", string.Empty).Replace("\"", string.Empty);
		}

		// Token: 0x040002DB RID: 731
		private const int Version = 1;

		// Token: 0x040002DC RID: 732
		private const string HelpArgument = "?";
	}
}
