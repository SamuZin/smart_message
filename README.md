# SMART MESSAGE

## Project Overview

**Smart Message** is an AI-powered application designed to monitor calendar events and automatically send personalized messages for special occasions. For example, on **Valentine's Day**, the system reads the event description, generates a heartfelt message using **LLaMA AI**, and sends it to the intended person automatically via **WhatsApp**.

This project integrates with the **Google Calendar API** to fetch events and uses **LLaMA AI** to generate personalized messages. It then sends those messages using **WhatsApp** through an integration with the **Twilio API** or other similar messaging platforms.

## Key Features

- **AI-Generated Personalized Messages**: Uses **LLaMA AI** to create meaningful, unique messages based on event descriptions.
- **Calendar Integration**: Automatically fetches events from **Google Calendar** and triggers the message generation.
- **WhatsApp Integration**: Sends the generated message directly to the recipient via **WhatsApp** using an API like **Twilio**.
- **Automated Process**: From generating the message to sending it, everything is fully automated.

## Architecture

The backend is built with **C#** and **ASP.NET Core**, ensuring a scalable and maintainable structure. The system interacts with the **Google Calendar API** to retrieve event details, then leverages **LLaMA AI** to generate the messages. Once the messages are created, they are sent via **WhatsApp** using the **Twilio API** (or an alternative service, if necessary).

For a more detailed view of the system architecture and API flow, you can explore the dashboard below.

<iframe width="768" height="432" src="https://miro.com/app/live-embed/uXjVIXwZJ7Q=/?moveToViewport=-1144,-1175,5103,2532&embedId=991078136096" frameborder="0" scrolling="no" allow="fullscreen; clipboard-read; clipboard-write" allowfullscreen></iframe>

## API Endpoints

This project exposes the following API endpoints:

- **[POST] create_message** – Generates a personalized message based on the event description.
- **[GET] get_event** – Retrieves details of upcoming events from the Google Calendar.
- **[POST] send_message** – Sends the generated message to the recipient via WhatsApp.

For detailed information on how these endpoints work, refer to the dashboard above, where the full flow of the API and system architecture is explained.

## Installation

Follow these steps to set up the project locally:

### 1. Clone the repository

```bash
git clone https://github.com/your-username/smart-message.git
```

### 2. Install dependencies
The project is built using .NET Core, so you need to have the .NET SDK installed. Install the necessary dependencies by running:

```bash
cd smart-message
dotnet restore
```

### 3. Set up Google Calendar API
To fetch events from your Google Calendar, follow the Google Calendar API Quickstart Guide to obtain the credentials file (credentials.json). Store the file securely and configure it in your project.

### 4. Set up LLaMA AI
The system uses LLaMA AI to generate personalized messages. To integrate LLaMA AI, you will need to either use an API wrapper for LLaMA or configure the necessary API calls in your project. Ensure that you have the proper API keys and model configurations.

### 5. Set up WhatsApp Integration
For sending messages via WhatsApp, the project uses the Twilio API. To set this up:

- Create a Twilio account at https://www.twilio.com/.
- Follow their documentation to set up WhatsApp messaging.
- Get your Twilio API keys and configure them in the project to enable WhatsApp message sending.

### 6. Run the application
Once everything is configured, run the application using:

```bash
dotnet run
```
### 7. Test the API
You can test the following API endpoints using tools like Postman or Insomnia:

- [POST] create_message – Generate a personalized message for an event.
- [GET] get_event – Retrieve upcoming events from the Google Calendar.
- [POST] send_message – Send the generated message via WhatsApp.

## Technologies Used
- C# & ASP.NET Core: For building the backend API.
- Google Calendar API: For retrieving events from the calendar.
- LLaMA AI: For generating personalized messages.
- Twilio API: For sending messages via WhatsApp (alternative services can be used if preferred).
- .NET Core: For backend development and API handling.

## Contributing
We welcome contributions! If you would like to improve this project, please consider:

- Forking the repository and submitting a pull request.
- Reporting issues or suggesting features via the GitHub Issues page.
- Helping with documentation and offering feedback.
