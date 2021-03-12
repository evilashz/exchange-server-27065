using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005E5 RID: 1509
	[ComVisible(true)]
	public class ExceptionHandlingClause
	{
		// Token: 0x060046DC RID: 18140 RVA: 0x001013F7 File Offset: 0x000FF5F7
		protected ExceptionHandlingClause()
		{
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060046DD RID: 18141 RVA: 0x001013FF File Offset: 0x000FF5FF
		public virtual ExceptionHandlingClauseOptions Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060046DE RID: 18142 RVA: 0x00101407 File Offset: 0x000FF607
		public virtual int TryOffset
		{
			get
			{
				return this.m_tryOffset;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060046DF RID: 18143 RVA: 0x0010140F File Offset: 0x000FF60F
		public virtual int TryLength
		{
			get
			{
				return this.m_tryLength;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060046E0 RID: 18144 RVA: 0x00101417 File Offset: 0x000FF617
		public virtual int HandlerOffset
		{
			get
			{
				return this.m_handlerOffset;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060046E1 RID: 18145 RVA: 0x0010141F File Offset: 0x000FF61F
		public virtual int HandlerLength
		{
			get
			{
				return this.m_handlerLength;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060046E2 RID: 18146 RVA: 0x00101427 File Offset: 0x000FF627
		public virtual int FilterOffset
		{
			get
			{
				if (this.m_flags != ExceptionHandlingClauseOptions.Filter)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Arg_EHClauseNotFilter"));
				}
				return this.m_filterOffset;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060046E3 RID: 18147 RVA: 0x00101448 File Offset: 0x000FF648
		public virtual Type CatchType
		{
			get
			{
				if (this.m_flags != ExceptionHandlingClauseOptions.Clause)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Arg_EHClauseNotClause"));
				}
				Type result = null;
				if (!MetadataToken.IsNullToken(this.m_catchMetadataToken))
				{
					Type declaringType = this.m_methodBody.m_methodBase.DeclaringType;
					Module module = (declaringType == null) ? this.m_methodBody.m_methodBase.Module : declaringType.Module;
					result = module.ResolveType(this.m_catchMetadataToken, (declaringType == null) ? null : declaringType.GetGenericArguments(), (this.m_methodBody.m_methodBase is MethodInfo) ? this.m_methodBody.m_methodBase.GetGenericArguments() : null);
				}
				return result;
			}
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x001014F4 File Offset: 0x000FF6F4
		public override string ToString()
		{
			if (this.Flags == ExceptionHandlingClauseOptions.Clause)
			{
				return string.Format(CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}, CatchType={5}", new object[]
				{
					this.Flags,
					this.TryOffset,
					this.TryLength,
					this.HandlerOffset,
					this.HandlerLength,
					this.CatchType
				});
			}
			if (this.Flags == ExceptionHandlingClauseOptions.Filter)
			{
				return string.Format(CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}, FilterOffset={5}", new object[]
				{
					this.Flags,
					this.TryOffset,
					this.TryLength,
					this.HandlerOffset,
					this.HandlerLength,
					this.FilterOffset
				});
			}
			return string.Format(CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}", new object[]
			{
				this.Flags,
				this.TryOffset,
				this.TryLength,
				this.HandlerOffset,
				this.HandlerLength
			});
		}

		// Token: 0x04001D13 RID: 7443
		private MethodBody m_methodBody;

		// Token: 0x04001D14 RID: 7444
		private ExceptionHandlingClauseOptions m_flags;

		// Token: 0x04001D15 RID: 7445
		private int m_tryOffset;

		// Token: 0x04001D16 RID: 7446
		private int m_tryLength;

		// Token: 0x04001D17 RID: 7447
		private int m_handlerOffset;

		// Token: 0x04001D18 RID: 7448
		private int m_handlerLength;

		// Token: 0x04001D19 RID: 7449
		private int m_catchMetadataToken;

		// Token: 0x04001D1A RID: 7450
		private int m_filterOffset;
	}
}
