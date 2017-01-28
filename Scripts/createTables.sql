CREATE TABLE Meetings(
    meeting_id uuid
    ,name text
    ,create_time timestamp
    ,start_fence timestamp
    ,end_fence timestamp
    ,length interval
    ,longitude real
    ,latitude real 
    ,location_description text
    ,PRIMARY KEY(meeting_id)
);

CREATE TABLE Meeting_Times (
    start_time timestamp
    ,meeting_id uuid
    ,PRIMARY KEY(start_time, meeting_id)
    ,FOREIGN KEY (meeting_id) REFERENCES Meetings ON DELETE CASCADE
);

CREATE TABLE Users (
    user_id uuid
    ,display_name text
    ,email text
    ,PRIMARY KEY (user_id)
);

CREATE TABLE Messages (
    message_id uuid
    ,meeting_id uuid
    ,user_id uuid
    ,content text
    ,created_time timestamp
    ,PRIMARY KEY(message_id, meeting_id, user_id)
    ,FOREIGN KEY (meeting_id) REFERENCES Meetings
    ,FOREIGN KEY (user_id) REFERENCES Users
);

CREATE TABLE Attendees (
    meeting_id uuid
    ,user_id uuid
    ,PRIMARY KEY (meeting_id, user_id)
    ,FOREIGN KEY (meeting_id) REFERENCES Meetings
    ,FOREIGN KEY (user_id) REFERENCES Users
);