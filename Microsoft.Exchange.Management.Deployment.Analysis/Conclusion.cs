using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000002 RID: 2
	public abstract class Conclusion
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		protected Conclusion()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		protected Conclusion(Result result)
		{
			this.name = result.Source.Name;
			this.value = (result.HasException ? null : result.ValueAsObject);
			this.valueType = result.Source.ValueType;
			this.exception = result.Exception;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002130 File Offset: 0x00000330
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002138 File Offset: 0x00000338
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.ThrowIfReadOnly();
				this.name = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002147 File Offset: 0x00000347
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000215E File Offset: 0x0000035E
		public object Value
		{
			get
			{
				if (this.HasException)
				{
					throw this.exception;
				}
				return this.value;
			}
			set
			{
				this.ThrowIfReadOnly();
				this.value = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000216D File Offset: 0x0000036D
		public object ValueOrNull
		{
			get
			{
				if (this.HasException)
				{
					return null;
				}
				return this.value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000217F File Offset: 0x0000037F
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002187 File Offset: 0x00000387
		public Type ValueType
		{
			get
			{
				return this.valueType;
			}
			set
			{
				this.ThrowIfReadOnly();
				this.valueType = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002196 File Offset: 0x00000396
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000219E File Offset: 0x0000039E
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.ThrowIfReadOnly();
				this.exception = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021AD File Offset: 0x000003AD
		public bool HasException
		{
			get
			{
				return this.Exception != null;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021BB File Offset: 0x000003BB
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021C3 File Offset: 0x000003C3
		public virtual void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021CC File Offset: 0x000003CC
		protected void ThrowIfReadOnly()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(Strings.CannotModifyReadOnlyProperty);
			}
		}

		// Token: 0x04000001 RID: 1
		private string name;

		// Token: 0x04000002 RID: 2
		private object value;

		// Token: 0x04000003 RID: 3
		private Type valueType;

		// Token: 0x04000004 RID: 4
		private Exception exception;

		// Token: 0x04000005 RID: 5
		private bool isReadOnly;
	}
}
