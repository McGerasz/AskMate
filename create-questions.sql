CREATE TABLE public.questions
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 ),
    title text NOT NULL,
    description text NOT NULL,
    submission_time date NOT NULL,
    PRIMARY KEY (id)
);
