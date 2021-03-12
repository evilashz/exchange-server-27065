using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Services.Common;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Management.ReportingWebService.PowerShell;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x0200001F RID: 31
	internal sealed class Entity : IEntity
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003723 File Offset: 0x00001923
		public Entity(string name, TaskInvocationInfo taskInvocationInfo, Dictionary<string, List<string>> reportPropertyCmdletParamsMap, IReportAnnotation annotation)
		{
			this.Name = name;
			this.TaskInvocationInfo = taskInvocationInfo;
			this.ReportPropertyCmdletParamsMap = reportPropertyCmdletParamsMap;
			this.Annotation = annotation;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003748 File Offset: 0x00001948
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003750 File Offset: 0x00001950
		public string Name { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003759 File Offset: 0x00001959
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003761 File Offset: 0x00001961
		public TaskInvocationInfo TaskInvocationInfo { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000376A File Offset: 0x0000196A
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003772 File Offset: 0x00001972
		public Dictionary<string, List<string>> ReportPropertyCmdletParamsMap { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000377B File Offset: 0x0000197B
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003783 File Offset: 0x00001983
		public IReportAnnotation Annotation { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000378C File Offset: 0x0000198C
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003794 File Offset: 0x00001994
		public string[] KeyMembers { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000379D File Offset: 0x0000199D
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000037A5 File Offset: 0x000019A5
		public Type ClrType { get; private set; }

		// Token: 0x060000A6 RID: 166 RVA: 0x000037B8 File Offset: 0x000019B8
		public void Initialize(IPSCommandResolver commandResolver)
		{
			ReadOnlyCollection<PSTypeName> outputType = commandResolver.GetOutputType(this.TaskInvocationInfo.CmdletName);
			this.ClrType = outputType[0].Type;
			this.KeyMembers = this.ClrType.GetCustomAttributes(typeof(DataServiceKeyAttribute), true).Cast<DataServiceKeyAttribute>().SelectMany((DataServiceKeyAttribute attr) => attr.KeyNames).ToArray<string>();
		}
	}
}
