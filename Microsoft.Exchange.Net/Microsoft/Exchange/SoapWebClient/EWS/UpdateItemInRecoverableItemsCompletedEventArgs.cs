using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004CB RID: 1227
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class UpdateItemInRecoverableItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600104E RID: 4174 RVA: 0x000274C6 File Offset: 0x000256C6
		internal UpdateItemInRecoverableItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000274D9 File Offset: 0x000256D9
		public UpdateItemInRecoverableItemsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateItemInRecoverableItemsResponseType)this.results[0];
			}
		}

		// Token: 0x040017E7 RID: 6119
		private object[] results;
	}
}
