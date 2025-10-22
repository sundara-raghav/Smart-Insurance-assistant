# Chatbot Setup Guide

## Overview
Your Smart Insurance Web application now includes a fully functional chatbot that appears as a floating widget on all pages.

## Current Status
✅ **Working**: The chatbot is fully functional with fallback responses
✅ **UI**: Modern, responsive chat interface
✅ **Integration**: Properly integrated into all pages

## Features
- **Floating Chat Widget**: Appears in bottom-right corner of all pages
- **Smart Responses**: Handles insurance-related questions intelligently
- **Fallback System**: Works even without external API configuration
- **Responsive Design**: Works on desktop and mobile devices
- **Session Integration**: Aware of logged-in users

## How to Use
1. **For Users**: Click the chat bubble icon in the bottom-right corner
2. **Ask Questions**: Type insurance-related questions
3. **Get Answers**: Receive helpful responses about policies, claims, coverage, etc.

## Optional: Enhanced AI Integration
To enable advanced AI responses using Google's Gemini API:

1. **Get API Key**: Visit [Google AI Studio](https://makersuite.google.com/app/apikey)
2. **Update Configuration**: Replace `YOUR_GEMINI_API_KEY_HERE` in `appsettings.json`
3. **Restart Application**: The chatbot will automatically use AI responses

## Supported Topics
The chatbot can help with:
- Health Insurance information
- Life Insurance policies
- Auto Insurance coverage
- Home Insurance protection
- Travel Insurance plans
- Claims process and filing
- Premium calculations
- General insurance questions

## Technical Details
- **Backend**: ASP.NET Core with ChatbotService
- **Frontend**: Vanilla JavaScript with modern UI
- **API Integration**: Google Gemini AI (optional)
- **Fallback**: Rule-based responses for reliability
- **Styling**: CSS animations and responsive design

## Files Modified/Created
- `Services/ChatbotService.cs` - Main chatbot logic
- `Pages/Chat.cshtml` - Chat page with instructions
- `Pages/Chat.cshtml.cs` - API endpoint for chat
- `wwwroot/js/chatbot.js` - Frontend chat widget
- `wwwroot/css/chatbot.css` - Chat styling
- `Pages/Shared/_Layout.cshtml` - Includes chat scripts

The chatbot is ready to use immediately with intelligent fallback responses!