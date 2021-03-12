using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200011A RID: 282
	internal sealed class FormDictionary
	{
		// Token: 0x06000963 RID: 2403 RVA: 0x00042C73 File Offset: 0x00040E73
		public FormDictionary() : this(FormDictionary.MatchMode.DowngradeMatch)
		{
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00042C7C File Offset: 0x00040E7C
		public FormDictionary(FormDictionary.MatchMode matchMode)
		{
			this.matchMode = matchMode;
			this.formDictionary = new Dictionary<FormKey, FormValue>();
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00042C96 File Offset: 0x00040E96
		public IEnumerable<FormKey> Keys
		{
			get
			{
				return this.formDictionary.Keys;
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00042CA3 File Offset: 0x00040EA3
		public bool ContainsKey(FormKey formKey)
		{
			return this.FindMatchingKeyClass(formKey, this.matchMode) != null;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00042CB8 File Offset: 0x00040EB8
		public bool ContainsKey(FormKey formKey, FormDictionary.MatchMode matchMode)
		{
			return this.FindMatchingKeyClass(formKey, matchMode) != null;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00042CC8 File Offset: 0x00040EC8
		public void Add(FormKey formKey, FormValue formValue)
		{
			this.formDictionary.Add(formKey, formValue);
		}

		// Token: 0x1700028F RID: 655
		public FormValue this[FormKey formKey]
		{
			get
			{
				string text = this.FindMatchingKeyClass(formKey, this.matchMode);
				if (text == null)
				{
					return null;
				}
				string @class = formKey.Class;
				formKey.Class = text;
				FormValue result = this.formDictionary[formKey];
				formKey.Class = @class;
				return result;
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00042D1C File Offset: 0x00040F1C
		private string FindMatchingKeyClass(FormKey formKey, FormDictionary.MatchMode matchMode)
		{
			ExTraceGlobals.FormsRegistryCallTracer.TraceDebug((long)this.GetHashCode(), "FormDictionary.FindMatchingKeyClass");
			if (this.formDictionary.ContainsKey(formKey))
			{
				ExTraceGlobals.FormsRegistryDataTracer.TraceDebug<string>((long)this.GetHashCode(), "FindMatchingKeyClass found exact key match, MessageClass = '{0}'", formKey.Class);
				return formKey.Class;
			}
			string text = null;
			if (matchMode == FormDictionary.MatchMode.DowngradeMatch)
			{
				ExTraceGlobals.FormsRegistryTracer.TraceDebug<string>((long)this.GetHashCode(), "FormDictionary is doing the down-grade lookup, MessageClass = '{0}'", formKey.Class);
				string @class = formKey.Class;
				string text2 = null;
				int num = @class.LastIndexOf('.');
				int num2;
				if (@class.StartsWith("REPORT.", StringComparison.OrdinalIgnoreCase))
				{
					num2 = @class.IndexOf('.', "REPORT.".Length);
					text2 = @class.Substring(num);
				}
				else
				{
					num2 = @class.IndexOf('.');
				}
				if (num2 < 0 || num2 == num)
				{
					return null;
				}
				string text3 = formKey.Class = @class.Substring(0, num);
				while (!this.formDictionary.ContainsKey(formKey))
				{
					num2 = ((text2 == null) ? text3.IndexOf('.') : text3.IndexOf('.', "REPORT.".Length));
					num = text3.LastIndexOf('.');
					if (num2 >= 0 && num2 != num)
					{
						formKey.Class = text3.Substring(0, num);
						text3 = formKey.Class;
						if (text2 == null)
						{
							continue;
						}
						string class2 = formKey.Class;
						formKey.Class += text2;
						if (!this.formDictionary.ContainsKey(formKey))
						{
							formKey.Class = class2;
							continue;
						}
						text = formKey.Class;
					}
					IL_17C:
					formKey.Class = @class;
					goto IL_183;
				}
				text = formKey.Class;
				goto IL_17C;
			}
			IL_183:
			ExTraceGlobals.FormsRegistryDataTracer.TraceDebug<string>((long)this.GetHashCode(), "FormDictionary is returning the down-graded MessageClass: '{0}'", text);
			return text;
		}

		// Token: 0x040006DB RID: 1755
		private Dictionary<FormKey, FormValue> formDictionary;

		// Token: 0x040006DC RID: 1756
		private FormDictionary.MatchMode matchMode;

		// Token: 0x0200011B RID: 283
		public enum MatchMode
		{
			// Token: 0x040006DE RID: 1758
			DowngradeMatch,
			// Token: 0x040006DF RID: 1759
			ExactMatch
		}
	}
}
