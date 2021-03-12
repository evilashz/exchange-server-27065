using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000A6 RID: 166
	internal class TaskInvocationInfo
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x000187D1 File Offset: 0x000169D1
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x000187D9 File Offset: 0x000169D9
		public string CommandName { get; private set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x000187E2 File Offset: 0x000169E2
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x000187EA File Offset: 0x000169EA
		public string InvocationName { get; private set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x000187F3 File Offset: 0x000169F3
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x000187FB File Offset: 0x000169FB
		public bool IsCmdletInvokedWithoutPSFramework { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00018804 File Offset: 0x00016A04
		public string DisplayName
		{
			get
			{
				if (this.CommandName != null)
				{
					return this.CommandName;
				}
				return this.InvocationName;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001881B File Offset: 0x00016A1B
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x00018823 File Offset: 0x00016A23
		public string ScriptName { get; private set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001882C File Offset: 0x00016A2C
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x00018834 File Offset: 0x00016A34
		public string RootScriptName { get; private set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001883D File Offset: 0x00016A3D
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x00018845 File Offset: 0x00016A45
		public string ShellHostName { get; private set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001884E File Offset: 0x00016A4E
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x00018856 File Offset: 0x00016A56
		public PropertyBag Fields { get; private set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001885F File Offset: 0x00016A5F
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x00018867 File Offset: 0x00016A67
		public string ParameterSetName { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00018870 File Offset: 0x00016A70
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x00018878 File Offset: 0x00016A78
		public PropertyBag UserSpecifiedParameters { get; private set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00018881 File Offset: 0x00016A81
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x00018889 File Offset: 0x00016A89
		public bool IsInternalOrigin { get; private set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00018892 File Offset: 0x00016A92
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x000188A3 File Offset: 0x00016AA3
		public bool IsVerboseOn
		{
			get
			{
				return TaskLogger.IsFileLoggingEnabled || this.isVerboseOn;
			}
			set
			{
				this.isVerboseOn = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x000188AC File Offset: 0x00016AAC
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x000188B4 File Offset: 0x00016AB4
		public bool IsDebugOn { get; set; }

		// Token: 0x060006B5 RID: 1717 RVA: 0x000188C0 File Offset: 0x00016AC0
		public TaskInvocationInfo(string commandName, string invocationName, string scriptName, string rootScriptName, bool isInternalOrigin, Dictionary<string, object> parameters, PropertyBag fields, string shellHostName)
		{
			this.CommandName = commandName;
			this.InvocationName = invocationName;
			this.ScriptName = scriptName;
			this.RootScriptName = rootScriptName;
			this.IsInternalOrigin = isInternalOrigin;
			if (parameters != null)
			{
				this.UpdateSpecifiedParameters(parameters);
			}
			this.Fields = fields;
			this.ShellHostName = shellHostName;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00018914 File Offset: 0x00016B14
		public static TaskInvocationInfo CreateForDirectTaskInvocation(string commandName)
		{
			PropertyBag fields = new PropertyBag();
			return new TaskInvocationInfo(commandName, commandName, null, null, false, null, fields, "PSDirectInvoke")
			{
				IsCmdletInvokedWithoutPSFramework = true,
				UserSpecifiedParameters = new PropertyBag()
			};
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001894C File Offset: 0x00016B4C
		public void UpdateSpecifiedParameters(Dictionary<string, object> boundParameters)
		{
			this.UserSpecifiedParameters = TaskInvocationInfo.GetUserSpecifiedParameters(boundParameters);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001895C File Offset: 0x00016B5C
		public void AddToUserSpecifiedParameter(object key, object value)
		{
			PropertyDefinition propertyDefinition = key as PropertyDefinition;
			if (propertyDefinition != null)
			{
				string name = propertyDefinition.Name;
				this.UserSpecifiedParameters[name] = value;
				return;
			}
			this.UserSpecifiedParameters[key] = value;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00018998 File Offset: 0x00016B98
		private static PropertyBag GetUserSpecifiedParameters(Dictionary<string, object> boundParameters)
		{
			PropertyBag propertyBag = new PropertyBag(boundParameters.Count);
			foreach (KeyValuePair<string, object> keyValuePair in boundParameters)
			{
				propertyBag.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return propertyBag;
		}

		// Token: 0x0400016B RID: 363
		private bool isVerboseOn;
	}
}
