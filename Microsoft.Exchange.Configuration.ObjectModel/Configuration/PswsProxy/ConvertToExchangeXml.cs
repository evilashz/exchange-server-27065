using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Configuration.PswsProxy
{
	// Token: 0x020000CA RID: 202
	[Cmdlet("ConvertTo", "ExchangeXml")]
	public class ConvertToExchangeXml : PSCmdlet
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0001BB77 File Offset: 0x00019D77
		public new ISessionState SessionState
		{
			get
			{
				if (this.sessionState == null)
				{
					this.sessionState = new PSSessionState(base.SessionState);
				}
				return this.sessionState;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0001BB98 File Offset: 0x00019D98
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x0001BBA0 File Offset: 0x00019DA0
		[Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
		[AllowNull]
		public PSObject InputObject { get; set; }

		// Token: 0x0600078C RID: 1932 RVA: 0x0001BBAC File Offset: 0x00019DAC
		protected override void BeginProcessing()
		{
			ExTraceGlobals.PublicPluginAPITracer.TraceDebug((long)this.GetHashCode(), "[ConvertToExchangeXml.BeginProcessing] Enter.");
			try
			{
				ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = ExchangePropertyContainer.GetExchangeRunspaceConfiguration(this.SessionState);
				this.serializer = new PSObjectSerializer((exchangeRunspaceConfiguration != null) ? exchangeRunspaceConfiguration.TypeTable : null);
				base.WriteObject("<?xml version=\"1.0\"?>");
				base.WriteObject("<Objs>");
			}
			catch (Exception ex)
			{
				ExTraceGlobals.PublicPluginAPITracer.TraceError<Exception>((long)this.GetHashCode(), "[ConvertToExchangeXml.BeginProcessing] Exception: {0}", ex);
				TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_PswsPublicAPIFailed, null, new object[]
				{
					"ConvertToExchangeXml.BeginProcessing",
					ex.ToString()
				});
				throw;
			}
			finally
			{
				ExTraceGlobals.PublicPluginAPITracer.TraceDebug((long)this.GetHashCode(), "[ConvertToExchangeXml.BeginProcessing] Exit.");
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001BC80 File Offset: 0x00019E80
		protected override void EndProcessing()
		{
			base.WriteObject("</Objs>");
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001BC90 File Offset: 0x00019E90
		protected override void ProcessRecord()
		{
			ExTraceGlobals.PublicPluginAPITracer.TraceDebug((long)this.GetHashCode(), "[ConvertToExchangeXml.ProcessRecord] Enter.");
			try
			{
				if (this.InputObject != null)
				{
					base.WriteObject(this.serializer.Serialize(this.InputObject));
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.PublicPluginAPITracer.TraceError<Exception>((long)this.GetHashCode(), "[ConvertToExchangeXml.ProcessRecord] Exception: {0}", ex);
				TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_PswsPublicAPIFailed, null, new object[]
				{
					"ConvertToExchangeXml.ProcessRecord",
					ex.ToString()
				});
				throw;
			}
			finally
			{
				ExTraceGlobals.PublicPluginAPITracer.TraceDebug((long)this.GetHashCode(), "[ConvertToExchangeXml.ProcessRecord] Exit.");
			}
		}

		// Token: 0x04000205 RID: 517
		private PSObjectSerializer serializer;

		// Token: 0x04000206 RID: 518
		private ISessionState sessionState;
	}
}
