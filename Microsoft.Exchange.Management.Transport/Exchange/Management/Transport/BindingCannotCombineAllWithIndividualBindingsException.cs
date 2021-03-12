using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000167 RID: 359
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class BindingCannotCombineAllWithIndividualBindingsException : LocalizedException
	{
		// Token: 0x06000F00 RID: 3840 RVA: 0x00035AC9 File Offset: 0x00033CC9
		public BindingCannotCombineAllWithIndividualBindingsException(string workLoad) : base(Strings.BindingCannotCombineAllWithIndividualBindings(workLoad))
		{
			this.workLoad = workLoad;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00035ADE File Offset: 0x00033CDE
		public BindingCannotCombineAllWithIndividualBindingsException(string workLoad, Exception innerException) : base(Strings.BindingCannotCombineAllWithIndividualBindings(workLoad), innerException)
		{
			this.workLoad = workLoad;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00035AF4 File Offset: 0x00033CF4
		protected BindingCannotCombineAllWithIndividualBindingsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.workLoad = (string)info.GetValue("workLoad", typeof(string));
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00035B1E File Offset: 0x00033D1E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("workLoad", this.workLoad);
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00035B39 File Offset: 0x00033D39
		public string WorkLoad
		{
			get
			{
				return this.workLoad;
			}
		}

		// Token: 0x04000670 RID: 1648
		private readonly string workLoad;
	}
}
