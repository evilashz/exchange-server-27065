using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001187 RID: 4487
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsGroupFoundMultipleTimesException : ExchangeSettingsException
	{
		// Token: 0x0600B694 RID: 46740 RVA: 0x002A01A0 File Offset: 0x0029E3A0
		public ExchangeSettingsGroupFoundMultipleTimesException(string name) : base(Strings.ExchangeSettingsGroupFoundMultipleTimes(name))
		{
			this.name = name;
		}

		// Token: 0x0600B695 RID: 46741 RVA: 0x002A01B5 File Offset: 0x0029E3B5
		public ExchangeSettingsGroupFoundMultipleTimesException(string name, Exception innerException) : base(Strings.ExchangeSettingsGroupFoundMultipleTimes(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600B696 RID: 46742 RVA: 0x002A01CB File Offset: 0x0029E3CB
		protected ExchangeSettingsGroupFoundMultipleTimesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600B697 RID: 46743 RVA: 0x002A01F5 File Offset: 0x0029E3F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17003995 RID: 14741
		// (get) Token: 0x0600B698 RID: 46744 RVA: 0x002A0210 File Offset: 0x0029E410
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040062FB RID: 25339
		private readonly string name;
	}
}
