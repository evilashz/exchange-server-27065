using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011DC RID: 4572
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutoAttendantExistsException : LocalizedException
	{
		// Token: 0x0600B928 RID: 47400 RVA: 0x002A57D7 File Offset: 0x002A39D7
		public AutoAttendantExistsException(string name, string dialplan) : base(Strings.AutoAttendantAlreadyExistsException(name, dialplan))
		{
			this.name = name;
			this.dialplan = dialplan;
		}

		// Token: 0x0600B929 RID: 47401 RVA: 0x002A57F4 File Offset: 0x002A39F4
		public AutoAttendantExistsException(string name, string dialplan, Exception innerException) : base(Strings.AutoAttendantAlreadyExistsException(name, dialplan), innerException)
		{
			this.name = name;
			this.dialplan = dialplan;
		}

		// Token: 0x0600B92A RID: 47402 RVA: 0x002A5814 File Offset: 0x002A3A14
		protected AutoAttendantExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.dialplan = (string)info.GetValue("dialplan", typeof(string));
		}

		// Token: 0x0600B92B RID: 47403 RVA: 0x002A5869 File Offset: 0x002A3A69
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("dialplan", this.dialplan);
		}

		// Token: 0x17003A35 RID: 14901
		// (get) Token: 0x0600B92C RID: 47404 RVA: 0x002A5895 File Offset: 0x002A3A95
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17003A36 RID: 14902
		// (get) Token: 0x0600B92D RID: 47405 RVA: 0x002A589D File Offset: 0x002A3A9D
		public string Dialplan
		{
			get
			{
				return this.dialplan;
			}
		}

		// Token: 0x04006450 RID: 25680
		private readonly string name;

		// Token: 0x04006451 RID: 25681
		private readonly string dialplan;
	}
}
