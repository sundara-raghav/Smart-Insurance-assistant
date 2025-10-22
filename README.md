# 🛡️ Smart Insurance Assistant Web Application

A comprehensive insurance management system built with **ASP.NET Core Razor Pages** and **C#**, featuring **Gemini AI chatbot**, local JSON storage, user/admin dashboards, policy management, claim processing, and intelligent recommendations with a **modern gradient UI**.

---

## 🚀 Features

### 🤖 **NEW: AI Chatbot Assistant**
- 💬 **Gemini AI Integration** - Powered by Google Gemini 1.5 Flash
- 🎯 **Context-Aware Responses** - Understands your policies and needs
- 📚 **Insurance Expertise** - Specialized in insurance guidance
- ⚡ **Instant Help** - 24/7 availability with floating chat widget
- 🎨 **Modern Design** - Beautiful purple gradient interface

### For Users
- ✅ **User Registration & Login** - Secure authentication system
- 📋 **Browse Insurance Policies** - View and compare policies by type
- 🎯 **Smart Recommendations** - AI-powered policy suggestions based on user profile
- 💰 **Risk-Based Premium Calculation** - Fair pricing based on age, gender, and risk factors
- 🛒 **Purchase Policies** - Buy insurance with calculated premiums
- 📝 **File Claims** - Submit insurance claims with detailed descriptions
- 📊 **Track Claims** - Monitor claim status (Pending/Approved/Rejected)
- 🔍 **Filtering & Sorting** - Advanced policy search capabilities
- 🤖 **Chat with AI** - Get instant answers to insurance questions

### For Administrators
- 🎛️ **Admin Dashboard** - Overview of system statistics with modern UI
- 📋 **Manage Policies** - Add, edit, delete, and activate/deactivate policies
- ⚖️ **Review Claims** - Approve or reject user claims with admin notes
- 👥 **Manage Users** - View, search, and delete user accounts
- 📊 **Reports & Statistics** - Comprehensive analytics and insights
  - Revenue tracking
  - Policy type distribution
  - Claim statistics
  - Top users by purchases
  - Recent activity monitoring

### 🎨 **UI Enhancements**
- ✨ **Modern Gradient Design** - Beautiful purple-to-pink color scheme
- 🎭 **Smooth Animations** - Hover effects, transitions, and page loads
- 📱 **Fully Responsive** - Works perfectly on all devices
- 🎯 **Enhanced Cards** - Lift-on-hover effects with shadows
- 🌈 **Gradient Buttons** - Eye-catching call-to-action elements

---

## 🏗️ Architecture

### Technology Stack
- **Framework**: ASP.NET Core 9.0 (Razor Pages)
- **Language**: C# 12
- **Storage**: JSON files (no database required)
- **Frontend**: Bootstrap 5, jQuery
- **Session Management**: ASP.NET Core Sessions

---

## 📦 Installation & Setup

### Prerequisites
- **.NET 8 or 9 SDK** - [Download here](https://dotnet.microsoft.com/download)
- **Visual Studio Code** (recommended) or Visual Studio 2022
- **C# Extension** for VS Code

### Installation Steps

1. **Navigate to the project directory**
   ```powershell
   cd "d:\C sharpe\SmartInsuranceWeb"
   ```

2. **Restore dependencies**
   ```powershell
   dotnet restore
   ```

3. **Build the project**
   ```powershell
   dotnet build
   ```

4. **Run the application**
   ```powershell
   dotnet run
   ```

5. **Open in browser**
   - Navigate to: `https://localhost:5001` or `http://localhost:5000`
   - Or press `Ctrl` and click the URL shown in terminal

---

## 🔑 Default Credentials

### Admin Account
- **Email**: `admin@insurance.com`
- **Password**: `Admin123!`

### Test User Accounts
- **User 1**
  - Email: `john@example.com`
  - Password: `User123!`

- **User 2**
  - Email: `jane@example.com`
  - Password: `User123!`

---

## 💡 Key Features Explained

### 1. Smart Policy Recommendations
The system analyzes user profiles (age, gender) and suggests relevant policies:
- Users under 30: Health and Life insurance
- Users 30-50: Auto and Home insurance  
- Users over 50: Health and Life insurance

### 2. Risk-Based Premium Calculation
Premiums are calculated dynamically based on:
- **Age factors**:
  - Under 25: +20% premium
  - Over 60: +50% premium
- **Gender factors**: Policy-specific adjustments
- **Base premium**: From policy configuration

### 3. Claim Processing Workflow
1. User files claim with description and amount
2. Claim appears in admin "Pending" queue
3. Admin reviews and approves/rejects with notes
4. User sees updated status on dashboard

### 4. JSON Storage
All data persists in JSON files:
- **Location**: `wwwroot/data/`
- **Format**: Pretty-printed JSON with indentation
- **Benefits**: No database setup, easy to inspect/edit

---

## 🎨 User Interface

### Navigation
- **Dynamic menu**: Changes based on user type (User/Admin)
- **Session-based**: Shows username and role
- **Responsive**: Works on mobile, tablet, and desktop

### Design Features
- Bootstrap 5 components
- Card-based layouts
- Modal dialogs for forms
- Color-coded status badges
- Hover effects and animations

---

## 🧪 Testing the Application

### Test User Workflow
1. Register a new account
2. Browse policies by type
3. View personalized recommendations
4. Purchase a policy
5. File a claim on purchased policy
6. Track claim status

### Test Admin Workflow
1. Login as admin
2. View dashboard statistics
3. Add/edit policies
4. Review and approve/reject claims
5. Manage users
6. Generate reports

---

## 📊 Sample Data Included

- **1 Admin** account
- **2 Sample users** with purchases
- **5 Insurance policies** (Health, Life, Auto, Home, Travel)
- **3 User purchases**
- **2 Sample claims** (1 approved, 1 pending)

---

## 🔧 Configuration

### Session Settings
Configured in `Program.cs`:
- **Timeout**: 30 minutes
- **HttpOnly cookies**: Enabled for security
- **Essential**: Required for application

### File Paths
- JSON files stored in: `wwwroot/data/`
- Automatically created on first run

---

## 📝 Development Notes

### Adding New Policies
1. Login as admin
2. Navigate to "Policies" → "Add New Policy"
3. Fill in details and features
4. Set age range and duration

### Extending Functionality
- **Add new policy types**: Update type dropdown in forms
- **Custom calculations**: Modify `PolicyService.CalculatePremium()`
- **New user fields**: Update `User.cs` model and registration form
- **Additional reports**: Extend `ReportsModel` with LINQ queries

---

## 🚦 Running in Production

### Build for Production
```powershell
dotnet publish -c Release -o ./publish
```

### Deploy
- Copy `publish` folder to server
- Ensure `wwwroot/data/` is writable
- Configure IIS or Kestrel
- Set environment to `Production`

---

## 🛠️ Troubleshooting

### Port Already in Use
```powershell
# Use different port
dotnet run --urls "http://localhost:5050"
```

### JSON File Not Found
- Files auto-created on first run
- Check `wwwroot/data/` directory exists
- Ensure application has write permissions

### Session Not Persisting
- Check browser allows cookies
- Verify session middleware is enabled
- Clear browser cache and retry

---

## 📚 Learning Resources

- [ASP.NET Core Documentation](https://learn.microsoft.com/aspnet/core)
- [Razor Pages Tutorial](https://learn.microsoft.com/aspnet/core/razor-pages)
- [Bootstrap 5 Docs](https://getbootstrap.com/docs/5.0)

---

## 🤝 Contributing

This is a learning project. Feel free to:
- Add new features
- Improve UI/UX
- Optimize calculations
- Add unit tests
- Enhance security

---

## 📄 License

This project is open source and available for educational purposes.

---


---

**Built with ❤️ using ASP.NET Core and C#**

*Last Updated: October 2025*
