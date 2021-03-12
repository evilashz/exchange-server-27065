using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200043A RID: 1082
	[DataContract]
	internal class DateRangeParameter : FormletParameter
	{
		// Token: 0x060035FF RID: 13823 RVA: 0x000A7685 File Offset: 0x000A5885
		public DateRangeParameter(string name, string[] taskParameterNames, LocalizedString dialogTitle, LocalizedString beforeDateDisplayTemplate, LocalizedString afterDateDisplayTemplate) : base(name, dialogTitle, LocalizedString.Empty, taskParameterNames)
		{
			this.locBeforeDateDisplayTemplate = beforeDateDisplayTemplate;
			this.locAfterDateDisplayTemplate = afterDateDisplayTemplate;
			base.FormletType = typeof(DateRangeModalEditor);
		}

		// Token: 0x17002124 RID: 8484
		// (get) Token: 0x06003600 RID: 13824 RVA: 0x000A76B5 File Offset: 0x000A58B5
		// (set) Token: 0x06003601 RID: 13825 RVA: 0x000A76C8 File Offset: 0x000A58C8
		[DataMember]
		public string BeforeDateDisplayTemplate
		{
			get
			{
				return this.locBeforeDateDisplayTemplate.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002125 RID: 8485
		// (get) Token: 0x06003602 RID: 13826 RVA: 0x000A76CF File Offset: 0x000A58CF
		// (set) Token: 0x06003603 RID: 13827 RVA: 0x000A76E2 File Offset: 0x000A58E2
		[DataMember]
		public string AfterDateDisplayTemplate
		{
			get
			{
				return this.locAfterDateDisplayTemplate.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040025C2 RID: 9666
		private LocalizedString locBeforeDateDisplayTemplate;

		// Token: 0x040025C3 RID: 9667
		private LocalizedString locAfterDateDisplayTemplate;
	}
}
