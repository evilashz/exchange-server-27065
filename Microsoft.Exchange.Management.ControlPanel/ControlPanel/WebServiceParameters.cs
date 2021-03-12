using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000060 RID: 96
	[DataContract]
	public abstract class WebServiceParameters : IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x06001A4B RID: 6731 RVA: 0x00054234 File Offset: 0x00052434
		protected WebServiceParameters()
		{
			this.InitializeDictionary();
		}

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x06001A4C RID: 6732
		public abstract string AssociatedCmdlet { get; }

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x06001A4D RID: 6733
		public abstract string RbacScope { get; }

		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x00054242 File Offset: 0x00052442
		// (set) Token: 0x06001A4F RID: 6735 RVA: 0x0005424A File Offset: 0x0005244A
		public bool IgnoreNullOrEmpty { get; set; }

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x00054253 File Offset: 0x00052453
		// (set) Token: 0x06001A51 RID: 6737 RVA: 0x00054266 File Offset: 0x00052466
		[DataMember]
		public virtual bool ShouldContinue
		{
			get
			{
				return this["Force"] != null;
			}
			set
			{
				if (value)
				{
					this.values["Force"] = new SwitchParameter(true);
				}
			}
		}

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x00054286 File Offset: 0x00052486
		// (set) Token: 0x06001A53 RID: 6739 RVA: 0x0005428E File Offset: 0x0005248E
		[DataMember]
		public virtual bool SuppressConfirm { get; set; }

		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x00054297 File Offset: 0x00052497
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x0005429F File Offset: 0x0005249F
		public virtual string SuppressConfirmParameterName { get; set; }

		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x000542A8 File Offset: 0x000524A8
		// (set) Token: 0x06001A57 RID: 6743 RVA: 0x000542B0 File Offset: 0x000524B0
		internal bool AllowExceuteThruHttpGetRequest { get; set; }

		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x000542B9 File Offset: 0x000524B9
		internal bool CanSuppressConfirm
		{
			get
			{
				return !string.IsNullOrEmpty(this.SuppressConfirmParameterName) && this.CanSetParameter(this.SuppressConfirmParameterName);
			}
		}

		// Token: 0x17001844 RID: 6212
		protected object this[PropertyDefinition cmdletParameterDefinition]
		{
			get
			{
				return this[cmdletParameterDefinition.Name];
			}
			set
			{
				this[cmdletParameterDefinition.Name] = value;
			}
		}

		// Token: 0x17001845 RID: 6213
		protected object this[string cmdletParameterName]
		{
			get
			{
				object result = null;
				this.values.TryGetValue(cmdletParameterName, out result);
				return result;
			}
			set
			{
				StringBuilder stringBuilder = new StringBuilder(this.AssociatedCmdlet, 80);
				stringBuilder.Append("?");
				stringBuilder.Append(cmdletParameterName);
				stringBuilder.Append(this.RbacScope);
				string role = stringBuilder.ToString();
				new PrincipalPermission(null, role).Demand();
				this.values[cmdletParameterName] = value;
			}
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00054394 File Offset: 0x00052594
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			if (this.IgnoreNullOrEmpty)
			{
				return (from entry in this.values
				where entry.Value != null && string.Empty != entry.Value as string
				select entry).GetEnumerator();
			}
			return this.values.GetEnumerator();
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x000543E7 File Offset: 0x000525E7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x000543EF File Offset: 0x000525EF
		protected bool ParameterIsSpecified(PropertyDefinition cmdletParameterDefinition)
		{
			return this.ParameterIsSpecified(cmdletParameterDefinition.Name);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000543FD File Offset: 0x000525FD
		protected bool ParameterIsSpecified(string cmdletParameterName)
		{
			return this.values.ContainsKey(cmdletParameterName);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0005440C File Offset: 0x0005260C
		protected bool CanSetParameter(string cmdletParameterName)
		{
			string role = this.AssociatedCmdlet + "?" + cmdletParameterName;
			return RbacPrincipal.Current.IsInRole(role);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00054442 File Offset: 0x00052642
		internal string[] GetRestrictedParameters(string[] parameters)
		{
			return (from x in parameters
			where !this.CanSetParameter(x)
			select x).ToArray<string>();
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0005445B File Offset: 0x0005265B
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.InitializeDictionary();
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00054463 File Offset: 0x00052663
		private void InitializeDictionary()
		{
			this.IgnoreNullOrEmpty = true;
			this.values = new Dictionary<string, object>();
		}

		// Token: 0x04001B11 RID: 6929
		private Dictionary<string, object> values;
	}
}
