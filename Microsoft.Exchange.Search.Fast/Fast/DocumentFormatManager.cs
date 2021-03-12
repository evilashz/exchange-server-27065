using System;
using System.Collections.Generic;
using Microsoft.Ceres.ContentEngine.Parsing.Component;
using Microsoft.Ceres.CoreServices.Tools.Management.Client;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000009 RID: 9
	internal sealed class DocumentFormatManager : FastManagementClient
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00003978 File Offset: 0x00001B78
		internal DocumentFormatManager(string serverName)
		{
			base.DiagnosticsSession.ComponentName = "DocumentFormatManager";
			base.DiagnosticsSession.Tracer = ExTraceGlobals.IndexManagementTracer;
			base.ConnectManagementAgents(serverName);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000039A7 File Offset: 0x00001BA7
		protected override int ManagementPortOffset
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000039AA File Offset: 0x00001BAA
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DocumentFormatManager>(this);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000039DC File Offset: 0x00001BDC
		public void EnableParsing(string format, bool enable)
		{
			Util.ThrowOnNullOrEmptyArgument(format, "format");
			base.PerformFastOperation(delegate()
			{
				this.parsingManagementService.EnableParsing(format, enable);
			}, "EnableParsing");
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003A50 File Offset: 0x00001C50
		public void RemoveFormat(string format)
		{
			Util.ThrowOnNullOrEmptyArgument(format, "format");
			base.PerformFastOperation(delegate()
			{
				this.parsingManagementService.RemoveFormat(format);
			}, "RemoveFormat");
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003ACC File Offset: 0x00001CCC
		public void AddFilterBasedFormat(string id, string name, string mime, string extension)
		{
			Util.ThrowOnNullOrEmptyArgument(id, "id");
			Util.ThrowOnNullOrEmptyArgument(name, "name");
			Util.ThrowOnNullOrEmptyArgument(mime, "mime");
			Util.ThrowOnNullOrEmptyArgument(extension, "extension");
			base.PerformFastOperation(delegate()
			{
				this.parsingManagementService.AddFilterBasedFormat(id, name, mime, extension);
			}, "AddFilterBasedFormat");
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003B69 File Offset: 0x00001D69
		public IList<FileFormatInfo> ListSupportedFormats()
		{
			return this.PerformFastOperation<IList<FileFormatInfo>>(() => this.parsingManagementService.ListSupportedFormats(), "ListSupportedFormats");
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public FileFormatInfo GetFormat(string formatId)
		{
			return this.PerformFastOperation<FileFormatInfo>(() => this.parsingManagementService.GetFormat(formatId), "GetFormat");
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003BDC File Offset: 0x00001DDC
		protected override void InternalConnectManagementAgents(WcfManagementClient client)
		{
			this.parsingManagementService = client.GetManagementAgent<IParsingManagementAgent>("Parsing/Admin");
		}

		// Token: 0x04000030 RID: 48
		private volatile IParsingManagementAgent parsingManagementService;
	}
}
