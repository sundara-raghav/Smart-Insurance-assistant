# 🚀 Quick Start Guide - Smart Insurance Assistant

## Application is Now Running! ✅

### Access the Application
🌐 **Open in your browser**: http://localhost:5074

---

## 🔑 Login Credentials

### Admin Account
- **Email**: `admin@insurance.com`
- **Password**: `Admin123!`
- **Access**: Full system management, policy creation, claim approval, reports

### Test User Accounts
**User 1:**
- **Email**: `john@example.com`
- **Password**: `User123!`
- Has 2 existing policies and 1 approved claim

**User 2:**
- **Email**: `jane@example.com`
- **Password**: `User123!`
- Has 1 life insurance policy and 1 pending claim

---

## 📋 Quick Test Scenarios

### 1. Test as Regular User
1. Click "Login" → Enter: `john@example.com` / `User123!`
2. View your dashboard with policies and claims
3. Click "Browse Policies" → See recommended policies
4. Try filtering by policy type (Health, Auto, etc.)
5. Click "My Claims" → File a new claim
6. Purchase a new policy

### 2. Test as Admin
1. Logout → Login as Admin: `admin@insurance.com` / `Admin123!`
2. View admin dashboard with statistics
3. Go to "Policies" → Add a new insurance policy
4. Go to "Claims" → Review and approve/reject pending claims
5. Go to "Users" → View all registered users
6. Go to "Reports" → See comprehensive analytics

### 3. Register New User
1. Logout → Click "Register"
2. Fill in your details (use your own email for testing)
3. Login with your new account
4. See personalized policy recommendations based on your age

---

## 🎯 Key Features to Try

### Premium Calculation
- Buy policies and notice how premiums change based on:
  - Your age (younger/older users pay different rates)
  - Gender (some policies have gender-based risk factors)
  - Policy type

### Smart Recommendations
- Users under 30 see: Health & Life insurance
- Users 30-50 see: Auto & Home insurance
- Users over 50 see: Health & Life insurance

### Claim Workflow
1. **User**: Files a claim on purchased policy
2. **Admin**: Reviews in "Manage Claims" → Pending tab
3. **Admin**: Approves/Rejects with notes
4. **User**: Sees updated status on dashboard

### Admin Reports
- Total revenue from all purchases
- Policy type distribution
- Claim approval rates
- Top users by purchases
- Recent activity

---

## 📁 Data Storage

All data is stored in JSON files located at:
```
d:\C sharpe\SmartInsuranceWeb\wwwroot\data\
```

You can inspect and manually edit:
- `admins.json` - Admin accounts
- `users.json` - User accounts
- `policies.json` - Insurance policies
- `claims.json` - Filed claims
- `purchases.json` - Policy purchases

---

## 🛑 Stopping the Application

Press `Ctrl+C` in the terminal to stop the server.

---

## 🔧 Troubleshooting

**Can't access the site?**
- Make sure the application is running (check terminal output)
- Try: http://localhost:5074
- Clear browser cache and retry

**Port already in use?**
```powershell
dotnet run --urls "http://localhost:5050"
```

**Data not saving?**
- Check `wwwroot/data/` folder exists
- Ensure application has write permissions

---

## 📚 Next Steps

1. **Explore the UI** - Try all features as both user and admin
2. **Review the Code** - Check `README.md` for architecture details
3. **Customize** - Modify policies, add new features, change styling
4. **Extend** - Add email notifications, payment gateway, etc.

---

## 💡 Tips

- Session timeout is 30 minutes
- Data persists between application restarts
- Bootstrap 5 is included for responsive design
- All forms have validation
- Modal dialogs for detailed views

---

**Enjoy exploring Smart Insurance Assistant! 🛡️**

*For detailed documentation, see README.md*
